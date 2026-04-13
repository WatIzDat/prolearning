using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProLearning.Api.Database;
using ProLearning.Api.Endpoints.Recommendations.Responses;

namespace ProLearning.Api.Endpoints.Recommendations;

public static class RecommendationsEndpoints
{
    public static void MapRecommendationsEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/recommendations");

        group.MapGet("", GetRecommendations);
    }

    public static async Task<Results<BadRequest, Ok<List<GetRecommendationsResponse>>>> GetRecommendations(ApplicationDbContext dbContext,
        string educationLevel,
        string[] interestAreas,
        int[] skillLevels,
        string[] goals,
        int limit)
    {
        
        if (interestAreas.Length != skillLevels.Length)
            return TypedResults.BadRequest();
    
        List<GetRecommendationsResponse> learningActivities =
            await dbContext.EducationLevels
                .Where(l => l.Name == educationLevel)
                .SelectMany(l => l.LearningActivities)
                .Select(a => new GetRecommendationsResponse
                {
                    Name = a.Name,
                    Score = 
                        a.InterestAreaScoreBoosts
                            .Where(e => ((IEnumerable<string>)interestAreas).Contains(e.InterestArea.Name) && skillLevels[interestAreas.ToList().IndexOf(e.InterestArea.Name)] == (int)e.SkillLevel)
                            .Select(e => e.Score)
                            .Sum() + 
                        a.GoalScoreBoosts
                            .Where(e => ((IEnumerable<string>)goals).Contains(e.Goal.Name))
                            .Select(e => e.Score)
                            .Sum(),
                    ScoreBreakdown = new GetRecommendationsResponse.ScoreBreakdownDto
                    {
                        InterestAreas = 
                            a.InterestAreaScoreBoosts
                                .Where(e => ((IEnumerable<string>)interestAreas).Contains(e.InterestArea.Name) && skillLevels[interestAreas.ToList().IndexOf(e.InterestArea.Name)] == (int)e.SkillLevel)
                                .Select(e => new GetRecommendationsResponse.ScoreBreakdownDto.InterestAreaScoreBreakdown { InterestArea = e.InterestArea.Name, SkillLevel = e.SkillLevel, Score = e.Score }),
                        Goals =
                            a.GoalScoreBoosts
                                .Where(e => ((IEnumerable<string>)goals).Contains(e.Goal.Name))
                                .Select(e => new GetRecommendationsResponse.ScoreBreakdownDto.GoalScoreBreakdown { Goal = e.Goal.Name, Score = e.Score })
                    }
                })
                .OrderBy(a => a.Score)
                .Take(limit)
                .ToListAsync();

        return TypedResults.Ok(learningActivities);
    }
}