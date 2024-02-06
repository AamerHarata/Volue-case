using AutoMapper;
using Volue_case.Models.Entities;
using Volue_case.Models.ViewModels;

namespace Volue_case.Models.AutoMapper.Profiles;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateProjection<Position, PossitionVm>();
    }
}