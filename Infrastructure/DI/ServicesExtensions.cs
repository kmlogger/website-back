using Domain.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

using Infrastructure.Repositories.Cold;
using Domain.Interfaces.Repositories.Cold;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure.DI;

public static class ServicesExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IDbCommit, DbCommit>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddHttpClient();
    }
    public static void AddDbContext(this IServiceCollection services)
        => services.AddDbContext<KMLoggerDbContex>(x => { x.UseNpgsql(Configuration.SqliteConnectionString);});
}
