using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SystemCase.Application.Dtos.ConfigurationDtos;
using SystemCase.Application.Extensions;
using SystemCase.Application.PipelineBehaviours;

namespace SystemCase.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(assembly);

        services.AddAutoMapper(assembly);

        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.Configure<MediatorTokenOptions>(configuration.GetSection("JwtTokenOption"));

        var tokenOption = configuration.GetSection("JwtTokenOption").Get<MediatorTokenOptions>();
        services.AddMediatorJwtAuth(tokenOption);

        return services;
    }
}