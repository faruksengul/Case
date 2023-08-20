using FluentValidation;
using SystemCase.Application.Cqrs.Commands.ReservationCommands;

namespace SystemCase.Application.Validators.ReservationValidators;

public class ReservationValidator:AbstractValidator<CreateReservationCommand>
{
    public ReservationValidator()
    {
        RuleFor(f => f.LastName).NotEmpty().MaximumLength(150)
            .WithMessage("Soyad boş geçilemez ve 150 karakter fazla olamaz");
        RuleFor(f => f.Name).NotEmpty().MaximumLength(150)
            .WithMessage("Ad boş geçilemez ve 150 karakterden fazla olamaz");
        RuleFor(f => f.ReservationDate).NotEmpty().WithMessage("Rezarvasyon tarihi boş geçilemez");
        RuleFor(f => f.Email).NotEmpty().MaximumLength(150)
            .WithMessage("Email boş geçilemez ve 150 karakterden fazla olamaz");
        RuleFor(f => f.NumberOfPerson).NotEmpty().WithMessage("Kişi sayısı boş geçilemez");

    }
}