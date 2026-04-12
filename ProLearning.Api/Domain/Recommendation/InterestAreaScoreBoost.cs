namespace ProLearning.Api.Domain.Recommendation;

public class InterestAreaScoreBoost
{
    public int LearningActivityId { get; init; }
    public int InterestAreaId { get; init; }
    public int Score { get; init; }
}