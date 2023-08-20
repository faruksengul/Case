using MediatR;
using SystemCase.Application.Dtos.CustomerDtos;
using SystemCase.Application.Wrappers;

namespace SystemCase.Application.Cqrs.Commands.CustomerCommands;

public class CreateCustomerCommand:IRequest<ApiResponse<CustomerDto>>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}