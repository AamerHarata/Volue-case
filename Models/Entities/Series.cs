using System.ComponentModel.DataAnnotations;

namespace Volue_case.Models.Entities;

public class Series
{
    public string ExternalId { get; set; }
    
    public BidStatus Status { get; set; }
    public string Direction { get; set; }
    public string Currency { get; set; }
    public string PriceArea { get; set; }
    public string AssetId { get; set; }
    public decimal Price { get; set; }
    public DateTime StartInterval { get; set; }
    public DateTime EndInterval { get; set; }
    public string Resolution { get; set; }
    public List<Position> Positions { get; set; }
    
    public Bid Bid { get; set; }
    public string BidExternalId { get; set; }
    
    public Customer Customer { get; set; }
    public string CustomerId { get; set; }
    
    
}


public enum Direction
{
    Undefined, Up, Down
}