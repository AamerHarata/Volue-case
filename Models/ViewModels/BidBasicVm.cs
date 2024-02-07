using System.Text.Json.Serialization;
using Volue_case.Models.Entities;

namespace Volue_case.Models.ViewModels;

public class BidBasicVm
{
    public string Id { get; private set; }
    public CustomerVm Customer { get; private set; }
    public string Day { get; private set; }
    [JsonIgnore] public DateTime DayAsDate { get; private set; }
    public string Market { get; private set; }
    public string Country { get; private set; }
    public BidStatus Status { get; private set; }
    public string DateOfLastChange { get; private set; }
    
    public decimal TotalValue { get; private set; }
    [JsonIgnore] public DateTime DateOfLastChangeAsDate { get; private set; }
}