using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProLearning.Api.Database;

namespace ProLearning.Api.Endpoints.EducationLevels;

public static class EducationLevelEndpoints
{
    public static void MapEducationLevelEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/educationlevel", GetEducationLevels);
    }

    public static async Task<Ok<List<EducationLevelDto>>> GetEducationLevels(ApplicationDbContext dbContext)
    {
        List<EducationLevelDto> educationLevels = await dbContext.EducationLevels
            .Select(l => new EducationLevelDto
            {
                Name = l.Name
            })
            .ToListAsync();

        return TypedResults.Ok(educationLevels);
    }
}