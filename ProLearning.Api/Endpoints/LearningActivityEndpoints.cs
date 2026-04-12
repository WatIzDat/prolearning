using Microsoft.EntityFrameworkCore;
using ProLearning.Api.ApiKey;
using ProLearning.Api.Database;
using ProLearning.Api.Domain;
using ProLearning.Api.Domain.Recommendation;
using ProLearning.Api.Requests;

namespace ProLearning.Api.Endpoints;

public static class LearningActivityEndpoints
{
    public static void MapLearningActivityEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/learningactivity").AddEndpointFilter<ApiKeyEndpointFilter>();

        group.MapGet("", GetLearningActivity);
        group.MapPost("", CreateLearningActivity);
        group.MapPut("{id:int}", UpdateLearningActivity);
        group.MapDelete("{id:int}", DeleteLearningActivity);
    }

    public static async Task<IResult> GetLearningActivity(ApplicationDbContext dbContext, int? id, string? name)
    {
        LearningActivity? learningActivity;
        
        if (id != null)
        {
            learningActivity =
                await dbContext.LearningActivities
                    .Include(a => a.EducationLevels)
                    .Include(a => a.InterestAreaScoreBoosts)
                        .ThenInclude(interestAreaScoreBoost => interestAreaScoreBoost.InterestArea)
                    .Include(a => a.GoalScoreBoosts)
                        .ThenInclude(goalScoreBoost => goalScoreBoost.Goal)
                    .Where(a => a.Id == id)
                    .FirstOrDefaultAsync();
        }
        else if (name != null)
        {
            learningActivity =
                await dbContext.LearningActivities
                    .Include(a => a.EducationLevels)
                    .Include(a => a.InterestAreaScoreBoosts)
                        .ThenInclude(interestAreaScoreBoost => interestAreaScoreBoost.InterestArea)
                    .Include(a => a.GoalScoreBoosts)
                        .ThenInclude(goalScoreBoost => goalScoreBoost.Goal)
                    .Where(a => a.Name == name)
                    .FirstOrDefaultAsync();
        }
        else
        {
            return Results.BadRequest();
        }

        return learningActivity == null ? 
            Results.NotFound() :
            Results.Ok(new
            {
                id = learningActivity.Id,
                name = learningActivity.Name,
                educationLevels = learningActivity.EducationLevels.Select(l => l.Name),
                interestAreaScoreBoosts = learningActivity.InterestAreaScoreBoosts.Select(b => new { interestArea = b.InterestArea.Name, score = b.Score }),
                goalScoreBoosts = learningActivity.GoalScoreBoosts.Select(b => new { goal = b.Goal.Name, score = b.Score })
            });
    }

    public static async Task<IResult> CreateLearningActivity(ApplicationDbContext dbContext,
        CreateLearningActivityRequest request)
    {
        LearningActivity learningActivity = await RequestToLearningActivity(dbContext, request);

        dbContext.LearningActivities.Add(learningActivity);
    
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }

    public static async Task<IResult> UpdateLearningActivity(ApplicationDbContext dbContext,
        int id,
        CreateLearningActivityRequest request)
    {
        LearningActivity? learningActivity =
            await dbContext.LearningActivities
                .Include(a => a.EducationLevels)
                .Include(a => a.InterestAreaScoreBoosts)
                .Include(a => a.GoalScoreBoosts)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        
        if (learningActivity == null)
            return Results.NotFound();
        
        LearningActivity requestLearningActivity = await RequestToLearningActivity(dbContext, request, id);
        
        learningActivity.Name = requestLearningActivity.Name;
        learningActivity.EducationLevels = requestLearningActivity.EducationLevels;
        learningActivity.InterestAreaScoreBoosts = requestLearningActivity.InterestAreaScoreBoosts;
        learningActivity.GoalScoreBoosts = requestLearningActivity.GoalScoreBoosts;
        
        await dbContext.SaveChangesAsync();
        
        return Results.NoContent();
    }

    public static async Task<IResult> DeleteLearningActivity(ApplicationDbContext dbContext, int id)
    {
        LearningActivity? learningActivity = await dbContext.LearningActivities.FindAsync(id);
        
        if (learningActivity == null)
            return Results.NotFound();

        dbContext.LearningActivities.Remove(learningActivity);

        await dbContext.SaveChangesAsync();
        
        return Results.NoContent();
    }

    private static async Task<LearningActivity> RequestToLearningActivity(ApplicationDbContext dbContext, CreateLearningActivityRequest request, int id = 0)
    {
        List<EducationLevel> educationLevels = await dbContext.EducationLevels
            .Where(l => request.EducationLevels.Contains(l.Name))
            .ToListAsync();

        List<string> interestAreaNames = request.InterestAreaScoreBoosts.Select(b => b.InterestArea).ToList();

        List<InterestArea> interestAreas = await dbContext.InterestAreas
            .Where(a => interestAreaNames.Contains(a.Name))
            .ToListAsync();

        List<InterestAreaScoreBoost> interestAreaScoreBoosts = interestAreas
            .Join(
                request.InterestAreaScoreBoosts,
                a => a.Name,
                b => b.InterestArea,
                (a, b) => new InterestAreaScoreBoost { InterestArea = a, Score = b.Score },
                StringComparer.OrdinalIgnoreCase)
            .ToList();
    
        List<string> goalNames = request.GoalScoreBoosts.Select(b => b.Goal).ToList();

        List<Goal> goals = await dbContext.Goals
            .Where(g => goalNames.Contains(g.Name))
            .ToListAsync();

        List<GoalScoreBoost> goalScoreBoosts = goals
            .Join(
                request.GoalScoreBoosts,
                g => g.Name,
                b => b.Goal,
                (g, b) => new GoalScoreBoost { Goal = g, Score = b.Score },
                StringComparer.OrdinalIgnoreCase)
            .ToList();
    
        LearningActivity learningActivity = new()
        {
            Id = id,
            Name = request.Name,
            EducationLevels = educationLevels,
            InterestAreaScoreBoosts = interestAreaScoreBoosts,
            GoalScoreBoosts = goalScoreBoosts
        };

        return learningActivity;
    }
}