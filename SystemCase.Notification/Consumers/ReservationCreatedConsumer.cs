using MassTransit;
using SystemCase.Notification.Services.Abstract;
using SystemCase.Shared.Messages;

namespace SystemCase.Notification.Consumers;

public class ReservationCreatedConsumer : IConsumer<ReservationCreatedMessage>
{
    private readonly IEmailService _emailService;

    public ReservationCreatedConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<ReservationCreatedMessage> context)
    {
        var message = context.Message;
        
        string subject = "Rezervasyon Onayınız";
        string body = $"Merhaba {message.Name} {message.Surname}, masa numaralarınız: {string.Join(", ", message.ReservedTableNumbers)}";

        await _emailService.SendEmailAsync(message.Email, subject, body);
    }
}