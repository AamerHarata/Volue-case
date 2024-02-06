using Volue_case.Data;

namespace Volue_case.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IBidRepository Bid { get;}
    public ICustomerRepository Customer { get; }



    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Bid = new BidRepository(context);
        Customer = new CustomerRepository(context);
    }

    public IRepository<T> Repository<T>() where T : class => new Repository<T>(_context);

    public async Task SaveAsync() => await _context.SaveChangesAsync();
    
    
    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}