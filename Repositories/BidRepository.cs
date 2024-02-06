using Microsoft.EntityFrameworkCore;
using Volue_case.Data;
using Volue_case.Models;
using Volue_case.Models.ViewModels;
using Volue_case.Services.CommonHelpers;

namespace Volue_case.Repositories;

public class BidRepository(ApplicationDbContext context) : Repository<Bid>(context), IBidRepository
{
    public IQueryable<Bid> GetById(string bidId) =>
        GetAll().Where(x => x.ExternalId == bidId);

    public IQueryable<UpdateHistory> GetHistoryByBidId(string bidId) =>
        context.History.Where(x => x.BidExternalId == bidId);

    public IQueryable<Bid> GetByDefaultQuery(BidDefaultQueryDto dto) =>
        context.Bids.AsNoTracking()
            .Where(x => x.Day.ToDateRequestFormat() == dto.Day 
                        && x.Market == dto.Market
                        && x.Country == dto.Country
                        && x.Series.Any(s=>s.CustomerId == dto.CustomerId));

    public IQueryable<Bid> GetAll() => context.Bids.AsNoTracking();
}