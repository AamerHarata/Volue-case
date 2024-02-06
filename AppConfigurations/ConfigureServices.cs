using Volue_case.Repositories;
using Volue_case.Services.BidResultService;
using Volue_case.Services.CustomerService;

namespace Volue_case.AppConfigurations;

public static class ConfigureServices
{
    public static void Configure(IServiceCollection services)
    {
        // Add unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Add repositories
        services.AddScoped<IBidRepository, BidRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        
        // Add services
        services.AddScoped<IBidResultService, BidResultService>();
        services.AddScoped<ICustomerService, CustomerService>();
        
        
    }
}