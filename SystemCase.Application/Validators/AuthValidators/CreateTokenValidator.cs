using FluentValidation;
using SystemCase.Application.Cqrs.Queries.AuthQueries;

namespace SystemCase.Application.Validators.AuthValidators;

public class CreateTokenValidator : AbstractValidator<CreateTokenQuery>
{
    public CreateTokenValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}