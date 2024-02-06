using Microsoft.EntityFrameworkCore;
using Volue_case.Models;
using Volue_case.Models.Entities;

namespace Volue_case.Data;

public static class DbContextConfigurations
{
    public static void ConfigureKeys(ModelBuilder builder)
    {
        builder.Entity<UpdateHistory>()
            .HasKey(x => new { BidId = x.BidExternalId, x.UpdateTime });
    }
    public static void ConfigureRelations(ModelBuilder builder)
    {
        builder.Entity<Bid>()
            .HasMany<Series>()
            .WithOne(x => x.Bid)
            .HasForeignKey(x => x.BidExternalId)
            .HasPrincipalKey(x => x.ExternalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Bid>()
            .HasMany<UpdateHistory>()
            .WithOne(x => x.Bid)
            .HasForeignKey(x=> x.BidExternalId)
            .HasPrincipalKey(x=> x.ExternalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Series>()
            .HasMany<Position>()
            .WithOne(x => x.Series)
            .HasForeignKey(x => x.SeriesExternalId)
            .HasPrincipalKey(x => x.ExternalId)
            .OnDelete(DeleteBehavior.Cascade)
            ;
    }
    
}