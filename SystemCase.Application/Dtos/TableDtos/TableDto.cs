using SystemCase.Domain.Core.Base.Concrete;

namespace SystemCase.Application.Dtos.TableDtos;

public class TableDto:BaseDto
{
    public int Number { get; set; }
    public int Capacity { get; set; }
}