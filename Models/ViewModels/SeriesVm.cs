namespace Volue_case.Models.ViewModels;

public class SeriesVm
{
    public string Id { get; set; }

    public BidStatus Status { get; set; }
    public string Direction { get; set; }
    public string Currency { get; set; }
    public string PriceArea { get; set; }
    public string AssetId { get; set; }
    public decimal Price { get; set; }
    public DateTime StartInterval { get; set; }
    public DateTime EndInterval { get; set; }
    public string Resolution { get; set; }
    public List<PossitionVm> Positions { get; set; }
    
}