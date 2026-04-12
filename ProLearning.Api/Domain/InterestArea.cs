using ProLearning.Api.Domain.Recommendation;

namespace ProLearning.Api.Domain;

public class InterestArea
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<LearningActivity> LearningActivities { get; } = [];
    public List<InterestAreaScoreBoost> InterestAreaScoreBoosts { get; } = [];
}