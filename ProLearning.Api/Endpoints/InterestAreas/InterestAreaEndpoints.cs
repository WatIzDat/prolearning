using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProLearning.Api.Database;

namespace ProLearning.Api.Endpoints.InterestAreas;

public static class InterestAreaEndpoints
{
    public static void MapInterestAreaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/interestarea", GetInterestAreas);
    }

    public static async Task<Ok<List<InterestAreaDto>>> GetInterestAreas(ApplicationDbContext dbContext)
    {
        List<InterestAreaDto> interestAreas = await dbContext.InterestAreas
            .Select(a => new InterestAreaDto
            {
                Name = a.Name,
            })
            .ToListAsync();
        
        return TypedResults.Ok(interestAreas);
    }
}