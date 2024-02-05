﻿using System.ComponentModel.DataAnnotations;

namespace Volue_case.Models;

public class Bid
{
    [Key]
    public string ExternalId { get; set; }
    public DateTime Day { get; set; }
    public DateTime DateOfLastChange { get; set; }
    public string Market { get; set; }
    public BidStatus Status { get; set; }
    public string Country { get; set; }
    
    public List<BidSeries> Series { get; set; }
    public List<UpdateHistory> UpdateHistory { get; set; }
}

public enum BidStatus
{
    Undefined,
    SentToTradingSystem
}