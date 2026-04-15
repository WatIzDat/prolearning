using FluentValidation;

namespace ProLearning.Api.Shared.Pagination;

public class PagedRequestValidator : AbstractValidator<PagedRequest>
{
    public PagedRequestValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).LessThanOrEqualTo(100);
    }
}