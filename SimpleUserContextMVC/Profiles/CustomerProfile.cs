using AutoMapper;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Models;

namespace SimpleUser.MVC.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<CustomerUpdateDto, CustomerDto>();
            CreateMap<CustomerDto, CustomerUpdateDto>();
            CreateMap<LoginDto, LoginViewModel>();
            CreateMap<LoginViewModel, LoginDto>();
        }
    }
}
