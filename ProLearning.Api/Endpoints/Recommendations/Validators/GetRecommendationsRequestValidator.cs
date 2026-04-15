using FluentValidation;
using ProLearning.Api.Endpoints.Recommendations.Requests;
using ProLearning.Api.Shared.Pagination;

namespace ProLearning.Api.Endpoints.Recommendations.Validators;

public class GetRecommendationsRequestValidator : AbstractValidator<GetRecommendationsRequest>
{
    public GetRecommendationsRequestValidator()
    {
        RuleFor(x => x.EducationLevel).NotEmpty();
        RuleFor(x => x.InterestAreas).NotEmpty();
        RuleFor(x => x.SkillLevels)
            .NotEmpty()
            .Must((r, x) => x.Length == r.InterestAreas.Length)
            .WithMessage("SkillLevels and InterestAreas must have the same length.");
        RuleFor(x => x.Goals).NotEmpty();
        RuleFor(x => x.PagedRequest).SetValidator(new PagedRequestValidator());
    }
}