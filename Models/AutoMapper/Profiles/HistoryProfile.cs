using AutoMapper;
using Volue_case.Models.Entities;
using Volue_case.Models.ViewModels;
using Volue_case.Services.CommonHelpers;

namespace Volue_case.Models.AutoMapper.Profiles;

public class HistoryProfile : Profile
{
    public HistoryProfile()
    {
        CreateProjection<UpdateHistory, HistoryVm>()
            .ForMember(dest => dest.DateTime, opt => 
                opt.MapFrom(dest => $"{dest.UpdateTime.ToDateFormat()} - {dest.UpdateTime.ToTimeFormat()}"))
            ;
    }
}