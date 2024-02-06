namespace Volue_case.Models.ViewModels;

public class BidVm
{
    public string Id { get; private set; }
    public DateTime Day { get; private set; }
    public DateTime DateOfLastChange { get; private set; }
    public string Market { get; private set; }
    public BidStatus Status { get; private set; }
    public string Country { get; private set; }

    public List<SeriesVm> Series { get; private set; } = new();
    public List<HistoryVm> UpdateHistory { get; private set; } = new();
}