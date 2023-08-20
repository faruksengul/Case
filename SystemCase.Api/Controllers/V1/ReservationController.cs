using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemCase.Api.Controllers.Base;
using SystemCase.Application.Cqrs.Commands.ReservationCommands;

namespace SystemCase.Api.Controllers.V1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReservationController:BaseController
{
    private readonly IMediator _mediator;

    public ReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateReservation(CreateReservationCommand table)
    {
        var response = await _mediator.Send(table);
        return ActionResultInstance(response);
    }
}