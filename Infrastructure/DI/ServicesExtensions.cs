using System;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

using Infrastructure.Repositories.Cold;
using Domain.Interfaces.Repositories.Cold;

namespace Infrastructure.DI;

public static class ServicesExtensions
{
    public static void ConfigureInfraServices(this IServiceCollection services)
    {
        services.AddScoped<IDbCommit, DbCommit>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddHttpClient();
    }
}
