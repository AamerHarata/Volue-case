using Volue_case.Models;
using Volue_case.Models.ViewModels;

namespace Volue_case.Services.BidResultService;

public interface IBidResultService
{
    Task<Bid?> FetchDataAsync(BidDefaultQueryDto dto);

    Task<Bid?> GetByIdAsync(string id);

    Task<string?> GetCustomerIdByBid(string bidId);
    Task<BidDetailedVm?> GetDetailedVmByIdAsync(string id);

    Task<List<Bid>> GetAllAsync();

    Task<List<BidBasicVm>> GetAllBasicVmAsync();

    Task<List<BidDetailedVm>> GetAllDetailedVmAsync();

    Task<List<HistoryVm>> GetHistoryAsync(string bidId);

    Task AddNewIfNotExist(Bid bid);

    Task DeleteById(string id);
    Task DeleteAllAsync();

    Task<bool> IsBidExistAsync(string id);
    Task<bool> AnyAsync();

    bool CheckTwoBidsIdentical(BidDetailedVm bid1, Bid bid2);
}