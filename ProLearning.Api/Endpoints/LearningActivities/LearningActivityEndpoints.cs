using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProLearning.Api.ApiKey;
using ProLearning.Api.Database;
using ProLearning.Api.Domain;
using ProLearning.Api.Domain.Recommendation;
using ProLearning.Api.Endpoints.LearningActivities.Responses;
using ProLearning.Api.Shared;

namespace ProLearning.Api.Endpoints.LearningActivities;

public static class LearningActivityEndpoints
{
    public static void MapLearningActivityEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/learningactivity")
            .AddEndpointFilter<ApiKeyEndpointFilter>()
            .ProducesProblem(400)
            .ProducesProblem(401);

        group.MapGet("", GetLearningActivity);
        group.MapPost("", CreateLearningActivity);
        group.MapPut("{id:int}", UpdateLearningActivity);
        group.MapDelete("{id:int}", DeleteLearningActivity);
    }

    public static async Task<Results<Ok<GetLearningActivityResponse>, BadRequest<HttpValidationProblemDetails>, NotFound>> GetLearningActivity(ApplicationDbContext dbContext, int? id, string? name)
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
            HttpValidationProblemDetails problemDetails = ValidationHelper.CreateValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "id", ["id or name must be specified."] }
            }, "/learningactivity");
            
            return TypedResults.BadRequest(problemDetails);
        }

        return learningActivity == null ? 
            TypedResults.NotFound() :
            TypedResults.Ok(new GetLearningActivityResponse
            {
                Id = learningActivity.Id,
                Name = learningActivity.Name,
                EducationLevels = learningActivity.EducationLevels
                    .Select(l => l.Name),
                InterestAreaScoreBoosts = learningActivity.InterestAreaScoreBoosts
                    .GroupBy(
                        b => b.InterestArea,
                        b => new LearningActivityDto.InterestAreaScoreBoost.SkillLevelScoreBoost
                        {
                            SkillLevel = b.SkillLevel, Score = b.Score
                        },
                        (key, g) => new LearningActivityDto.InterestAreaScoreBoost
                        {
                            InterestArea = key.Name, SkillLevelScoreBoosts = g.ToArray()
                        }),
                GoalScoreBoosts = learningActivity.GoalScoreBoosts
                    .Select(b => new LearningActivityDto.GoalScoreBoost
                    {
                        Goal = b.Goal.Name,
                        Score = b.Score
                    })
            });
    }

    public static async Task<Results<NoContent, BadRequest<HttpValidationProblemDetails>>> CreateLearningActivity(ApplicationDbContext dbContext,
        IValidator<LearningActivityDto> validator,
        LearningActivityDto dto)
    {
        ValidationResult? validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            HttpValidationProblemDetails problemDetails =
                ValidationHelper.CreateValidationProblemDetails(validationResult.ToDictionary(), "/learningactivity");

            return TypedResults.BadRequest(problemDetails);
        }
        
        LearningActivity learningActivity = await MapLearningActivityDto(dbContext, dto);

        dbContext.LearningActivities.Add(learningActivity);
    
        await dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound, BadRequest<HttpValidationProblemDetails>>> UpdateLearningActivity(ApplicationDbContext dbContext,
        int id,
        IValidator<LearningActivityDto> validator,
        LearningActivityDto dto)
    {
        ValidationResult? validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            HttpValidationProblemDetails problemDetails =
                ValidationHelper.CreateValidationProblemDetails(validationResult.ToDictionary(), "/learningactivity");

            return TypedResults.BadRequest(problemDetails);
        }
        
        LearningActivity? learningActivity =
            await dbContext.LearningActivities
                .Include(a => a.EducationLevels)
                .Include(a => a.InterestAreaScoreBoosts)
                .Include(a => a.GoalScoreBoosts)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        
        if (learningActivity == null)
            return TypedResults.NotFound();
        
        LearningActivity requestLearningActivity = await MapLearningActivityDto(dbContext, dto, id);
        
        learningActivity.Name = requestLearningActivity.Name;
        learningActivity.EducationLevels = requestLearningActivity.EducationLevels;
        learningActivity.InterestAreaScoreBoosts = requestLearningActivity.InterestAreaScoreBoosts;
        learningActivity.GoalScoreBoosts = requestLearningActivity.GoalScoreBoosts;
        
        await dbContext.SaveChangesAsync();
        
        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> DeleteLearningActivity(ApplicationDbContext dbContext, int id)
    {
        LearningActivity? learningActivity = await dbContext.LearningActivities.FindAsync(id);
        
        if (learningActivity == null)
            return TypedResults.NotFound();

        dbContext.LearningActivities.Remove(learningActivity);

        await dbContext.SaveChangesAsync();
        
        return TypedResults.NoContent();
    }

    private static async Task<LearningActivity> MapLearningActivityDto(ApplicationDbContext dbContext, LearningActivityDto dto, int id = 0)
    {
        List<EducationLevel> educationLevels = await dbContext.EducationLevels
            .Where(l => dto.EducationLevels.Contains(l.Name))
            .ToListAsync();

        List<string> interestAreaNames = dto.InterestAreaScoreBoosts.Select(b => b.InterestArea).ToList();

        List<InterestArea> interestAreas = await dbContext.InterestAreas
            .Where(a => interestAreaNames.Contains(a.Name))
            .ToListAsync();

        List<InterestAreaScoreBoost> interestAreaScoreBoosts = interestAreas
            .Join(
                dto.InterestAreaScoreBoosts,
                a => a.Name,
                b => b.InterestArea,
                (a, b) => b.SkillLevelScoreBoosts.Select(slb => new InterestAreaScoreBoost { InterestArea = a, SkillLevel = slb.SkillLevel, Score = slb.Score }),
                StringComparer.OrdinalIgnoreCase)
            .SelectMany(x => x)
            .ToList();
    
        List<string> goalNames = dto.GoalScoreBoosts.Select(b => b.Goal).ToList();

        List<Goal> goals = await dbContext.Goals
            .Where(g => goalNames.Contains(g.Name))
            .ToListAsync();

        List<GoalScoreBoost> goalScoreBoosts = goals
            .Join(
                dto.GoalScoreBoosts,
                g => g.Name,
                b => b.Goal,
                (g, b) => new GoalScoreBoost { Goal = g, Score = b.Score },
                StringComparer.OrdinalIgnoreCase)
            .ToList();
    
        LearningActivity learningActivity = new()
        {
            Id = id,
            Name = dto.Name,
            EducationLevels = educationLevels,
            InterestAreaScoreBoosts = interestAreaScoreBoosts,
            GoalScoreBoosts = goalScoreBoosts
        };

        return learningActivity;
    }
}