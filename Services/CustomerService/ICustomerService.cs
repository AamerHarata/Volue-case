namespace Volue_case.Services.CustomerService;

public interface ICustomerService
{
    Task AddNewIfNotExistAsync(string customerId);
    Task DeleteAllAsync();
}