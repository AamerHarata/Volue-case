﻿using AutoMapper;
using Volue_case.Models.ViewModels;

namespace Volue_case.Models.AutoMapper.Profiles;

public class BidProfile : Profile
{
    public BidProfile()
    {
        CreateProjection<Bid, BidVm>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.ExternalId));
            ;
    }
}