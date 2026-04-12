namespace ProLearning.Api.Domain.Recommendation;

public class GoalScoreBoost
{
    public int LearningActivityId { get; init; }
    public int GoalId { get; init; }
    public int Score { get; init; }
}