using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemCase.Api.Controllers.Base;
using SystemCase.Application.Cqrs.Commands.TableCommands;

namespace SystemCase.Api.Controllers.V1;


[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TableController:BaseController
{
    private readonly IMediator _mediator;

    public TableController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTable(CreateTableCommand table)
    {
        var response = await _mediator.Send(table);
        return ActionResultInstance(response);
    }
}