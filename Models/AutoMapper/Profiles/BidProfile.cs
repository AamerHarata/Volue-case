using AutoMapper;
using Volue_case.Models.ViewModels;
using Volue_case.Services.CommonHelpers;

namespace Volue_case.Models.AutoMapper.Profiles;

public class BidProfile : Profile
{
    public BidProfile()
    {
        CreateProjection<Bid, BidBasicVm>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.Customer,
                opt => opt.MapFrom(src => src.Series.Select(s=> s.Customer).FirstOrDefault()))
            .ForMember(dest => dest.Day, 
                opt => opt.MapFrom(dest => dest.Day.ToDateFormat()))
            .ForMember(dest => dest.DayAsDate,
                opt => opt.MapFrom(dest => dest.Day))
            .ForMember(dest => dest.DateOfLastChange,
                opt => opt.MapFrom(dest => 
                    $"{dest.DateOfLastChange.ToDateFormat()} - {dest.DateOfLastChange.ToTimeFormat()}"))
            .ForMember(dest => dest.DateOfLastChangeAsDate,
                opt => opt.MapFrom(dest => dest.DateOfLastChange))
            .ForMember(dest => dest.TotalValue,
                opt => opt.MapFrom(dest => dest.Series.Sum(x=>x.Price)))
            ;
        CreateProjection<Bid, BidDetailedVm>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.ExternalId))
            .ForMember(dest => dest.Customer,
                opt => opt.MapFrom(src => src.Series.Select(s => s.Customer).FirstOrDefault()))
            .ForMember(dest => dest.Day,
                opt => opt.MapFrom(dest => dest.Day.ToDateFormat()))
            .ForMember(dest => dest.DayAsDate,
                opt => opt.MapFrom(dest => dest.Day))
            .ForMember(dest => dest.DateOfLastChange,
                opt => opt.MapFrom(dest =>
                    $"{dest.DateOfLastChange.ToDateFormat()} - {dest.DateOfLastChange.ToTimeFormat()}"))
            .ForMember(dest => dest.DateOfLastChangeAsDate,
                opt => opt.MapFrom(dest => dest.DateOfLastChange))
            .ForMember(dest => dest.TotalValue,
                opt => opt.MapFrom(dest => dest.Series.Sum(x => x.Price)))
            ;
    }
}