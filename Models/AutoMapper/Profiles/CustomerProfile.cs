using AutoMapper;
using Volue_case.Models.Entities;
using Volue_case.Models.ViewModels;

namespace Volue_case.Models.AutoMapper.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateProjection<Customer, CustomerVm>();
    }
}