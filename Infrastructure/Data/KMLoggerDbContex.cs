using System;
using System.Reflection;
using Domain.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Cold;

public class KMLoggerDbContex : DbContext
{
    public KMLoggerDbContex(DbContextOptions<KMLoggerDbContex> options) : base(options) { }
    public DbSet<User> Users { get; init; }
    public DbSet<Role> Roles { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<Notification>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
