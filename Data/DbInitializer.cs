using Microsoft.EntityFrameworkCore;

namespace Volue_case.Data;

public static class DbInitializer
{
    public static async Task InitializeDatabase(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var service = scope.ServiceProvider;
        var context = service.GetRequiredService<ApplicationDbContext>();
        await ExecuteInitialize(context);
    }

    private static async Task ExecuteInitialize(DbContext context)
    {
        await context.Database.MigrateAsync();
    }
}