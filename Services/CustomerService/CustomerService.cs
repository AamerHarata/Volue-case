using Microsoft.EntityFrameworkCore;
using Volue_case.Models.Entities;
using Volue_case.Repositories;

namespace Volue_case.Services.CustomerService;

public class CustomerService(IUnitOfWork unitOfWork) : ICustomerService
{
    public async Task AddNewIfNotExistAsync(string customerId)
    {
        if (await IsCustomerExistAsync(customerId))
            return;
        await unitOfWork.Customer.AddAsync(new Customer(customerId));
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAllAsync() =>
        unitOfWork.Customer.DeleteAll(await unitOfWork.Customer.GetAll().ToListAsync());

    private async Task<bool> IsCustomerExistAsync(string customerId) => 
        await unitOfWork.Customer.GetById(customerId).AnyAsync();
}