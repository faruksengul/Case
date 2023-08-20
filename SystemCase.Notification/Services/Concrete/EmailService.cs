using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using SystemCase.Notification.Models;
using SystemCase.Notification.Services.Abstract;

namespace SystemCase.Notification.Services.Concrete;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettingsOptions)
    {
        _smtpSettings = smtpSettingsOptions.Value;
    }

    public async Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        using (var client = new SmtpClient(_smtpSettings.Host, Convert.ToInt16(_smtpSettings.Port)))
        {
            client.Credentials = new NetworkCredential(_smtpSettings.User, _smtpSettings.Password);
            client.EnableSsl = true;
            //client.UseDefaultCredentials = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.User),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }

        return true;
    }
}