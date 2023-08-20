using FluentValidation;
using SystemCase.Application.Cqrs.Commands.AuthCommands;

namespace SystemCase.Application.Validators.AuthValidators;

public class CreateTokenByRefreshTokenValidator : AbstractValidator<CreateTokenByRefreshTokenCommand>
{
    public CreateTokenByRefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}