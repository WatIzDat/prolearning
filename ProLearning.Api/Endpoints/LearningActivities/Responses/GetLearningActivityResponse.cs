namespace ProLearning.Api.Endpoints.LearningActivities.Responses;

public class GetLearningActivityResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> EducationLevels { get; set; }
    public IEnumerable<LearningActivityDto.InterestAreaScoreBoost> InterestAreaScoreBoosts { get; set; }
    public IEnumerable<LearningActivityDto.GoalScoreBoost> GoalScoreBoosts { get; set; }
}