namespace ProLearning.Api.Domain.Recommendation;

public class InterestAreaScoreBoost
{
    public int LearningActivityId { get; init; }
    public LearningActivity LearningActivity { get; init; }
    public int InterestAreaId { get; init; }
    public InterestArea InterestArea { get; init; }
    public int Score { get; init; }
}