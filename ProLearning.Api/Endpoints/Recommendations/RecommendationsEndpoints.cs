using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProLearning.Api.Database;
using ProLearning.Api.Endpoints.Recommendations.Requests;
using ProLearning.Api.Endpoints.Recommendations.Responses;
using ProLearning.Api.Shared;
using ProLearning.Api.Shared.Pagination;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ProLearning.Api.Endpoints.Recommendations;

public static class RecommendationsEndpoints
{
    public static void MapRecommendationsEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/recommendations");

        group.MapGet("", GetRecommendations).WithName("GetRecommendations");
    }

    public static async Task<Results<BadRequest<HttpValidationProblemDetails>, Ok<PagedList<GetRecommendationsResponse>>>> GetRecommendations(ApplicationDbContext dbContext,
        IValidator<GetRecommendationsRequest> validator,
        string[] interestAreas,
        int[] skillLevels,
        string[] goals,
        string educationLevel = "",
        // int limit = 0,
        int page = 1,
        int pageSize = 0)
    {
        GetRecommendationsRequest request = new()
        {
            EducationLevel = educationLevel,
            InterestAreas = interestAreas,
            SkillLevels = skillLevels,
            Goals = goals,
            PagedRequest = new PagedRequest
            {
                Page = page,
                PageSize = pageSize
            }
        };
        
        ValidationResult? validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            HttpValidationProblemDetails problemDetails =
                ValidationHelper.CreateValidationProblemDetails(validationResult.ToDictionary(), "/recommendations");

            return TypedResults.BadRequest(problemDetails);
        }
        
        int interestAreasCount = await dbContext.InterestAreas.Where(a => ((IEnumerable<string>)interestAreas).Contains(a.Name)).CountAsync();

        if (interestAreasCount != interestAreas.Length)
        {
            HttpValidationProblemDetails problemDetails =
                ValidationHelper.CreateValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "interestAreas", ["One or more interest areas is invalid."]}
                }, "/recommendations");

            return TypedResults.BadRequest(problemDetails);
        }
        
        int goalsCount = await dbContext.Goals.Where(g => ((IEnumerable<string>)goals).Contains(g.Name)).CountAsync();

        if (goalsCount != goals.Length)
        {
            HttpValidationProblemDetails problemDetails =
                ValidationHelper.CreateValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "goals", ["One or more goals is invalid."]}
                }, "/recommendations");

            return TypedResults.BadRequest(problemDetails);
        }

        IOrderedQueryable<GetRecommendationsResponse> learningActivitiesQueryable =
            dbContext.EducationLevels
                .Where(l => l.Name == educationLevel)
                .SelectMany(l => l.LearningActivities)
                .Select(a => new GetRecommendationsResponse
                {
                    Name = a.Name,
                    Url = a.Url,
                    Score =
                        a.InterestAreaScoreBoosts
                            .Where(e => ((IEnumerable<string>)interestAreas).Contains(e.InterestArea.Name) &&
                                        skillLevels[interestAreas.ToList().IndexOf(e.InterestArea.Name)] ==
                                        (int)e.SkillLevel)
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
                                .Where(e => ((IEnumerable<string>)interestAreas).Contains(e.InterestArea.Name) &&
                                            skillLevels[interestAreas.ToList().IndexOf(e.InterestArea.Name)] ==
                                            (int)e.SkillLevel)
                                .Select(e => new GetRecommendationsResponse.ScoreBreakdownDto.InterestAreaScoreBreakdown
                                    { InterestArea = e.InterestArea.Name, SkillLevel = e.SkillLevel, Score = e.Score }),
                        Goals =
                            a.GoalScoreBoosts
                                .Where(e => ((IEnumerable<string>)goals).Contains(e.Goal.Name))
                                .Select(e => new GetRecommendationsResponse.ScoreBreakdownDto.GoalScoreBreakdown
                                    { Goal = e.Goal.Name, Score = e.Score })
                    }
                })
                .OrderByDescending(a => a.Score);
        
        PagedList<GetRecommendationsResponse> learningActivities = 
            await PagedList<GetRecommendationsResponse>.CreateAsync(learningActivitiesQueryable, page, pageSize);
                
        return TypedResults.Ok(learningActivities);
    }
}