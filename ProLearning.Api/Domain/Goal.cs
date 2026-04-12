namespace ProLearning.Api.Domain;

public class Goal
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<LearningActivity> LearningActivities { get; } = [];
}