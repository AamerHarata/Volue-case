using Volue_case.Models.Entities;

namespace Volue_case.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    IQueryable<Customer> GetById(string customerId);
    IQueryable<Customer> GetAll();
}