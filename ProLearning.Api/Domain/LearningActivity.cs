using ProLearning.Api.Domain.Recommendation;

namespace ProLearning.Api.Domain;

public class LearningActivity
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<EducationLevel> EducationLevels { get; init; } = [];
    public List<InterestArea> InterestAreas { get; } = [];
    public List<InterestAreaScoreBoost> InterestAreaScoreBoosts { get; init; } = [];
    public List<Goal> Goals { get; } = [];
    public List<GoalScoreBoost> GoalScoreBoosts { get; init; } = [];
}