using Microsoft.EntityFrameworkCore;
using Volue_case.Data;

namespace Volue_case.AppConfigurations;

public static class ConfigureDb
{
    public static void Configure(IServiceCollection services, string? connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Connection string is failed");
        
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
    }
}