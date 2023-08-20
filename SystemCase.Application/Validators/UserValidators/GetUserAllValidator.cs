using FluentValidation;
using SystemCase.Application.Cqrs.Queries.UserQueries;

namespace SystemCase.Application.Validators.UserValidators;
public class GetUserAllValidator : AbstractValidator<GetUserAllQuery>
{
    public GetUserAllValidator()
    {
        RuleFor(x => x.PageId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.OrderType)
            .Must(orderType => orderType!.Equals("ascending") || orderType.Equals("descending"))
            .When(x => !string.IsNullOrEmpty(x.OrderType));
    }
}