using Microsoft.EntityFrameworkCore;
using Volue_case.Models;
using Volue_case.Models.Entities;

namespace Volue_case.Data;

public static class DbContextConfigurations
{
    public static void ConfigureKeys(ModelBuilder builder)
    {
        builder.Entity<Bid>()
            .HasKey(x => x.ExternalId);
        builder.Entity<Series>()
            .HasKey(x => x.ExternalId);
        builder.Entity<Position>()
            .HasKey(x => x.Id);
        builder.Entity<UpdateHistory>()
            .HasKey(x => new { BidId = x.BidExternalId, x.UpdateTime });
    }
    public static void ConfigureRelations(ModelBuilder builder)
    {
        builder.Entity<Bid>()
            .HasMany(x=>x.Series)
            .WithOne(x => x.Bid)
            .HasForeignKey(x => x.BidExternalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Bid>()
            .HasMany(x=> x.UpdateHistory)
            .WithOne(x => x.Bid)
            .HasForeignKey(x=> x.BidExternalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Series>()
            .HasMany(x=> x.Positions)
            .WithOne(x => x.Series)
            .HasForeignKey(x => x.SeriesExternalId)
            .OnDelete(DeleteBehavior.Cascade)
            ;
    }
    
}