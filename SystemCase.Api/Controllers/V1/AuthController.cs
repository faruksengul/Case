using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemCase.Api.Controllers.Base;
using SystemCase.Application.Cqrs.Commands.AuthCommands;
using SystemCase.Application.Cqrs.Queries.AuthQueries;

namespace SystemCase.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : BaseController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> CreateToken(CreateTokenQuery request)
    {
        var response = await _mediator.Send(request);

        return ActionResultInstance(response);
    }

    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> CreateTokenByRefreshToken(CreateTokenByRefreshTokenCommand request)
    {
        var response = await _mediator.Send(request);

        return ActionResultInstance(response);
    }
}