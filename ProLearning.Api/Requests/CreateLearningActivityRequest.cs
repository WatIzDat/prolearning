namespace ProLearning.Api.Requests;

public class CreateLearningActivityRequest
{
    public string Name { get; set; }
    public List<string> EducationLevels { get; set; }
    public InterestAreaScoreBoost[] InterestAreaScoreBoosts { get; set; }
    public GoalScoreBoost[] GoalScoreBoosts { get; set; }

    public class InterestAreaScoreBoost
    {
        public string InterestArea { get; set; }
        public int Score { get; set; }
    }
    
    public class GoalScoreBoost
    {
        public string Goal { get; set; }
        public int Score { get; set; }
    }
}