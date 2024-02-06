using Volue_case.Repositories;
using Volue_case.Services.BidResultService;

namespace Volue_case.AppConfigurations;

public static class ConfigureServices
{
    public static void Configure(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBidRepository, BidRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IBidResultService, BidResultService>();
        
        
    }
}