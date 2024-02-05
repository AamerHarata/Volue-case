using Volue_case.Models.Entities;

namespace Volue_case.Models;

public class BidSeries
{
    public string ExternalId { get; set; }
    public string CustomerId { get; set; }
    public BidStatus Status { get; set; }
    public string Direction { get; set; }
    public string Currency { get; set; }
    public string PriceArea { get; set; }
    public string AssetId { get; set; }
    public int Price { get; set; }
    public DateTime StartInterval { get; set; }
    public DateTime EndInterval { get; set; }
    public string Resolution { get; set; }
    public List<Position> Positions { get; set; }
    
    public Bid Bid { get; set; }
    public string BidExternalId { get; set; }
    
    
}


public enum Direction
{
    Undefined, Up, Down
}