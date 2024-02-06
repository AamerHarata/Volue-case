namespace Volue_case.Models.ViewModels;

public class BidDetailedVm : BidBasicVm
{
    public List<SeriesVm> Series { get; private set; } = new();
    public List<HistoryVm> UpdateHistory { get; private set; } = new();
}