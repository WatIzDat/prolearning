using ProLearning.Api.Domain.Recommendation;

namespace ProLearning.Api.Endpoints.LearningActivities;

public class LearningActivityDto
{
    public string Name { get; set; }
    public string Url { get; set; }
    public List<string> EducationLevels { get; set; }
    public InterestAreaScoreBoost[] InterestAreaScoreBoosts { get; set; }
    public GoalScoreBoost[] GoalScoreBoosts { get; set; }

    public class InterestAreaScoreBoost
    {
        public string InterestArea { get; set; }
        public SkillLevelScoreBoost[] SkillLevelScoreBoosts { get; set; }

        public class SkillLevelScoreBoost
        {
            public SkillLevel SkillLevel { get; set; }
            public int Score { get; set; }
        }
    }
    
    public class GoalScoreBoost
    {
        public string Goal { get; set; }
        public int Score { get; set; }
    }
}