namespace Volue_case.Models;

public class UpdateHistory
{
    public DateTime UpdateTime { get; set; }
    public BidStatus FromStatus { get; set; }
    public BidStatus ToStatus { get; set; }
    
    public Bid Bid { get; set; }
    public string BidExternalId { get; set; }
    
}