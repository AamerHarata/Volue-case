using Microsoft.EntityFrameworkCore;
using Volue_case.Models;
using Volue_case.Models.Entities;

namespace Volue_case.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<UpdateHistory> History { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        DbContextConfigurations.ConfigureKeys(builder);
        DbContextConfigurations.ConfigureRelations(builder);
    }
    
}