namespace ProLearning.Api.Domain;

public class LearningActivity
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<EducationLevel> EducationLevels { get; } = [];
    public List<InterestArea> InterestAreas { get; } = [];
    public List<Goal> Goals { get; } = [];
}