using System;
using System.Reflection;
using Domain.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Cold;

public class ColdDbContext : DbContext
{
    public ColdDbContext(DbContextOptions<ColdDbContext> options) : base(options) { }
    public DbSet<User> Users { get; init; }
    public DbSet<Role> Roles { get; init; }
    public DbSet<LogApp> Logs {get; init;}
    public DbSet<App> Apps {get; init;}
    public DbSet<Category> Categories {get; init;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<Notification>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
