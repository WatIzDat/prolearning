using ProLearning.Api.Domain.Recommendation;

namespace ProLearning.Api.Endpoints.Recommendations.Responses;

public class GetRecommendationsResponse
{
    public string Name { get; set; }
    public string Url { get; set; }
    public int Score { get; set; }
    public ScoreBreakdownDto ScoreBreakdown { get; set; }

    public class ScoreBreakdownDto
    {
        public IEnumerable<InterestAreaScoreBreakdown> InterestAreas { get; set; }
        public IEnumerable<GoalScoreBreakdown> Goals { get; set; }
        
        public class InterestAreaScoreBreakdown
        {
            public string InterestArea { get; set; }
            public SkillLevel SkillLevel { get; set; }
            public int Score { get; set; }
        }
        
        public class GoalScoreBreakdown
        {
            public string Goal { get; set; }
            public int Score { get; set; }
        }
    }
}