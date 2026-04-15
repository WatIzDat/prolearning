using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProLearning.Api.Database;

namespace ProLearning.Api.Endpoints.Goals;

public static class GoalEndpoints
{
    public static void MapGoalEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/goal", GetGoals);
    }

    public static async Task<Ok<List<GoalDto>>> GetGoals(ApplicationDbContext dbContext)
    {
        List<GoalDto> goals = await dbContext.Goals
            .Select(g => new GoalDto
            {
                Name = g.Name,
            })
            .ToListAsync();
        
        return TypedResults.Ok(goals);
    }
}