using MassTransit;
using SystemCase.Notification.Consumers;
using SystemCase.Notification.Models;
using SystemCase.Notification.Services.Abstract;
using SystemCase.Notification.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
// Add services to the container.
builder.Services.AddScoped<IEmailService,EmailService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ReservationCreatedConsumer>();
        
    x.UsingRabbitMq((context, cfg) => 
    {
        cfg.ReceiveEndpoint("reservation-created-queue", e => 
        {
            e.ConfigureConsumer<ReservationCreatedConsumer>(context);
        });
    });
});
    
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();