using AutoMapper;

namespace Volue_case.Models.AutoMapper.Profiles;

public class BidProfile : Profile
{
    public BidProfile()
    {
        CreateMap<string, Bid>();
    }
}