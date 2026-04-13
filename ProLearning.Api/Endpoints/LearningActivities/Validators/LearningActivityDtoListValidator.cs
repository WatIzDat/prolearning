using FluentValidation;

namespace ProLearning.Api.Endpoints.LearningActivities.Validators;

public class LearningActivityDtoListValidator : AbstractValidator<List<LearningActivityDto>>
{
    public LearningActivityDtoListValidator()
    {
        RuleForEach(x => x).SetValidator(new LearningActivityDtoValidator());
    }
}