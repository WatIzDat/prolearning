using FluentValidation;

namespace ProLearning.Api.Endpoints.LearningActivities.Validators;

public class LearningActivityDtoValidator : AbstractValidator<LearningActivityDto>
{
    public LearningActivityDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Url).NotEmpty();
        RuleFor(x => x.EducationLevels).NotEmpty();
        RuleFor(x => x.InterestAreaScoreBoosts).NotNull();
        RuleFor(x => x.GoalScoreBoosts).NotNull();
    }
}