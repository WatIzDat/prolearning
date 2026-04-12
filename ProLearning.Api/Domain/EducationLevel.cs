namespace ProLearning.Api.Domain;

public class EducationLevel
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string[] Grades { get; init; }
    public List<LearningActivity> LearningActivities { get; init; }
}