using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data.Cold;

public  class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KMLoggerDbContex>
{
    public KMLoggerDbContex CreateDbContext(string[] args)
    {
        try
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CONNECTION_STRING")))
                throw new Exception("A connection string must be provided.");
                
            var builder = new DbContextOptionsBuilder<KMLoggerDbContex>();
            builder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            var context = new KMLoggerDbContex(builder.Options);
            return context;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
}
