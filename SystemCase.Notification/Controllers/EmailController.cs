using Microsoft.AspNetCore.Mvc;
using SystemCase.Notification.Models;
using SystemCase.Notification.Services.Abstract;

namespace SystemCase.Notification.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class EmailController : Controller
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<ActionResult> SendEmail(EmailPostModel model)
    {
        var response = await _emailService.SendEmailAsync(model.ToEmail, model.Subject, model.Body);
        if (response)
            return Ok();
        return BadRequest();
    }
}