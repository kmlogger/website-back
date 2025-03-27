using System;
using System.Reflection;
using Domain.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class KMLoggerDbContex : DbContext
{
    public KMLoggerDbContex(DbContextOptions<KMLoggerDbContex> options) : base(options) { }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<Notification>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
