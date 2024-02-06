namespace Volue_case.Models;

public class UpdateHistory
{
    public DateTime UpdateTime { get; set; }
    public BidStatus FromStatus { get; set; }
    public BidStatus ToStatus { get; set; }
    
    public Bid Bid { get; set; }
    public string BidId { get; set; }
    
    // ToDo :: Set foreign key to Bid
    // ToDo :: Set primary key for this class (Combined key with BidId and TimeStamp works well).
}