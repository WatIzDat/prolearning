namespace ProLearning.Api.Domain.Recommendation;

public class GoalScoreBoost
{
    public int LearningActivityId { get; init; }
    public LearningActivity LearningActivity { get; init; }
    public int GoalId { get; init; }
    public Goal Goal { get; init; }
    public int Score { get; init; }
}