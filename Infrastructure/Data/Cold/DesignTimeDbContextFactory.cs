using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data.Cold;

public  class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ColdDbContext>
{
    public ColdDbContext CreateDbContext(string[] args)
    {
        try
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("COLD_CONNECTION_STRING")))
                throw new Exception("A connection string must be provided.");
                
            var builder = new DbContextOptionsBuilder<ColdDbContext>();
            builder.Us(Environment.GetEnvironmentVariable("COLD_CONNECTION_STRING"));
            var context = new ColdDbContext(builder.Options);
            return context;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
}
