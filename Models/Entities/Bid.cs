namespace Volue_case.Models.Entities;

public class Bid
{
    public string ExternalId { get; set; }
    public DateTime Day { get; set; }
    public DateTime DateOfLastChange { get; set; }
    public string Market { get; set; }
    public BidStatus Status { get; set; }
    public string Country { get; set; }

    public List<Series> Series { get; set; } = new();
    public List<UpdateHistory> UpdateHistory { get; set; } = new();
}

public enum BidStatus
{
    Undefined,
    SentToTradingSystem,
    PendingConfirmation,
    PulledByAamer
}