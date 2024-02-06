using Volue_case.Models;
using Volue_case.Models.ViewModels;

namespace Volue_case.Repositories;

public interface IBidRepository : IRepository<Bid>
{
    IQueryable<Bid> GetById(string bidId);

    IQueryable<UpdateHistory> GetHistoryByBidId(string bidId);
    IQueryable<Bid> GetByDefaultQuery(BidDefaultQueryDto dto);
    IQueryable<Bid> GetAll();

}