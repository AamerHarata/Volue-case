using Volue_case.Models.Entities;

namespace Volue_case.Models.ViewModels;

public class HistoryVm
{
    public string DateTime { get; private set;}
    public BidStatus FromStatus { get; private set; }
    public BidStatus ToStatus { get; private set; }
}