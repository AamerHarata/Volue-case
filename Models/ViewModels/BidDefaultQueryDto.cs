using System.Text.Json.Serialization;
using Volue_case.Services.CommonHelpers;

namespace Volue_case.Models.ViewModels;

public class BidDefaultQueryDto
{
    public BidDefaultQueryDto(string customerId,  DateTime day, string market, string country)
    {
        Day = day.ToDateRequestFormat();
        Market = market;
        CustomerId = customerId;
        Country = country;
    }
    public BidDefaultQueryDto(){}
    public string Day { get; private set; }
    public string Market { get; private set; }
    public string CustomerId { get; private set; }
    public string Country { get; private set; } 
    
}