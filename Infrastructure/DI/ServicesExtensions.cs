using System;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

using ILogRepositoryHot = Domain.Interfaces.Repositories.Hot.ILogRepository;
using ILogRepositoryCold = Domain.Interfaces.Repositories.Cold.ILogRepository;
using LogRepositoryCold = Infrastructure.Repositories.Cold.LogRepository;
using LogRepositoryHot = Infrastructure.Repositories.Hot.LogRepository;

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
        services.AddScoped<ILogRepositoryHot, LogRepositoryHot>();
        services.AddScoped<ILogRepositoryCold, LogRepositoryCold>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAppRepository, AppRepository>();
        services.AddHttpClient();
    }
}
