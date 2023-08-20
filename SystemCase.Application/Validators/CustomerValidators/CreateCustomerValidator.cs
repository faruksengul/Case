using FluentValidation;
using SystemCase.Application.Cqrs.Commands.CustomerCommands;

namespace SystemCase.Application.Validators.CustomerValidators;

public class CreateCustomerValidator:AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(f => f.Name).NotEmpty().MaximumLength(150)
            .WithMessage("İsim boş geçilemez ve max 150 karakter olabilir");
        RuleFor(f => f.LastName).NotEmpty().MaximumLength(150)
            .WithMessage("Soy isim boş geçilemez ve max 150 karakter olabilir");
        RuleFor(f => f.Email).NotEmpty().MaximumLength(150)
            .WithMessage("Email boş geçilemez ve max 150 karakter olabilir");
    }
}