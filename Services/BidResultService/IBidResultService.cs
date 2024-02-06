using Volue_case.Models;
using Volue_case.Models.ViewModels;

namespace Volue_case.Services.BidResultService;

public interface IBidResultService
{
    Task<Bid?> FetchDataAsync(BidDefaultQueryDto dto);

    Task<Bid?> GetByIdAsync(string id);

    Task<List<Bid>> GetAllAsync();

    Task AddNewIfNotExist(Bid bid);
}