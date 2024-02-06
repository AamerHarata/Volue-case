using Microsoft.EntityFrameworkCore;
using Volue_case.Data;
using Volue_case.Models.Entities;

namespace Volue_case.Repositories;

public class CustomerRepository(ApplicationDbContext context) : Repository<Customer>(context), ICustomerRepository
{
    public IQueryable<Customer> GetById(string customerId) =>
        GetAll().Where(x => x.Id == customerId);

    public IQueryable<Customer> GetAll() => context.Customers.AsNoTracking();
}