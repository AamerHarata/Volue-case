using AutoMapper;
using Volue_case.Models.Entities;
using Volue_case.Models.ViewModels;

namespace Volue_case.Models.AutoMapper.Profiles;

public class SeriesProfile : Profile
{
    public SeriesProfile()
    {
        CreateProjection<Series, SeriesVm>()
            .ForMember(dest => dest.Id, 
                opt =>
                    opt.MapFrom(src => src.ExternalId));
            ;
    }
}