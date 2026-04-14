using ProLearning.Api.Domain.Recommendation;

namespace ProLearning.Api.Domain;

public class LearningActivity
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Url { get; set; }
    public List<EducationLevel> EducationLevels { get; set; } = [];
    public List<InterestArea> InterestAreas { get; } = [];
    public List<InterestAreaScoreBoost> InterestAreaScoreBoosts { get; set; } = [];
    public List<Goal> Goals { get; } = [];
    public List<GoalScoreBoost> GoalScoreBoosts { get; set; } = [];
}