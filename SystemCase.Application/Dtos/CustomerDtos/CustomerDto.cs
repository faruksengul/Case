using SystemCase.Domain.Core.Base.Concrete;

namespace SystemCase.Application.Dtos.CustomerDtos;

public class CustomerDto:BaseDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}