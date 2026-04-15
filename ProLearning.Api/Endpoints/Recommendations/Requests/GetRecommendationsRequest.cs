using ProLearning.Api.Shared.Pagination;

namespace ProLearning.Api.Endpoints.Recommendations.Requests;

public class GetRecommendationsRequest
{
    public string EducationLevel { get; set; }
    public string[] InterestAreas { get; set; }
    public int[] SkillLevels { get; set; }
    public string[] Goals { get; set; }
    public PagedRequest PagedRequest { get; set; }
}