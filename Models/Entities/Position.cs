namespace Volue_case.Models.Entities;

public class Position
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public decimal Quantity { get; set; }
    
    public Series Series { get; set; }
    public string SeriesExternalId { get; set; }
}