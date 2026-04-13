namespace ProLearning.Api.Endpoints.Recommendations.Requests;

public class GetRecommendationsRequest
{
    public string EducationLevel { get; set; }
    public string[] InterestAreas { get; set; }
    public int[] SkillLevels { get; set; }
    public string[] Goals { get; set; }
    public int Limit { get; set; }
}