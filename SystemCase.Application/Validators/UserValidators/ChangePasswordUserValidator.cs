using FluentValidation;
using SystemCase.Application.Cqrs.Commands.UserCommands;

namespace SystemCase.Application.Validators.UserValidators;

public class ChangePasswordUserValidator : AbstractValidator<ChangePasswordUserCommand>
{
    public ChangePasswordUserValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty();
    }
}