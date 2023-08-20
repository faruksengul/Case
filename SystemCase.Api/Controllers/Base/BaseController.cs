using System.Net;
using Microsoft.AspNetCore.Mvc;
using SystemCase.Application.Wrappers;

namespace SystemCase.Api.Controllers.Base;

public class BaseController:ControllerBase
{
    public IActionResult ActionResultInstance<TData>(ApiResponse<TData> response) where TData : class
    {
        List<int> allowedHttpStatusReturnCodes = new() {
            (int)HttpStatusCode.OK,
            (int)HttpStatusCode.Created,
            (int)HttpStatusCode.NoContent,
            (int)HttpStatusCode.BadRequest,
            (int)HttpStatusCode.NotFound
        };

        return new ObjectResult(response)
        {
            StatusCode = allowedHttpStatusReturnCodes.Any(allowedHttpStatusReturnCode => allowedHttpStatusReturnCode.Equals(response.StatusCode))
                ? response.StatusCode
                : (int)HttpStatusCode.Forbidden
        };
    }
}