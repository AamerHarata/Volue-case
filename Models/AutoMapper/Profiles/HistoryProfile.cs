using AutoMapper;
using Volue_case.Models.ViewModels;

namespace Volue_case.Models.AutoMapper.Profiles;

public class HistoryProfile : Profile
{
    public HistoryProfile()
    {
        CreateProjection<UpdateHistory, HistoryVm>();
    }
}