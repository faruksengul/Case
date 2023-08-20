using MediatR;
using SystemCase.Application.Dtos.CustomerDtos;
using SystemCase.Application.Dtos.ReservationDtos;
using SystemCase.Application.Wrappers;

namespace SystemCase.Application.Cqrs.Commands.ReservationCommands;

public class CreateReservationCommand:IRequest<ApiResponse<ReservationDto>>
{

    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int NumberOfPerson { get; set; }
    public DateTime ReservationDate { get; set; }
}