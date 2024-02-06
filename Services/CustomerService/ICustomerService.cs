namespace Volue_case.Services.CustomerService;

public interface ICustomerService
{
    Task AddNewIfNotExist(string customerId);
}