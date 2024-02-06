namespace Volue_case.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    IBidRepository Bid { get; }
    ICustomerRepository Customer { get; }
    IRepository<T> Repository<T>() where T : class;
    Task SaveAsync();
}