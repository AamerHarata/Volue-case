using Volue_case.Services.CommonHelpers;

namespace Volue_case.Models.ViewModels;

public class BidDefaultQueryDto(string customerId, DateTime day, string market , string country)
{
    public string Day { get; private set; } = day.ToDateRequestFormat();
    public string Market { get; private set; } = market;
    public string CustomerId { get; private set; } = customerId;
    public string Country { get; private set; } = country;
}