using FluentValidation;
using SystemCase.Application.Cqrs.Commands.TableCommands;

namespace SystemCase.Application.Validators.TableValidators;

public class TableValidator:AbstractValidator<CreateTableCommand>
{
    public TableValidator()
    {
        RuleFor(f => f.Capacity).NotEmpty().WithMessage("Kapasite boş geçilemez");
        RuleFor(f => f.TableType).NotEmpty().WithMessage("Masa tipi boş geçilemez");
    }
}