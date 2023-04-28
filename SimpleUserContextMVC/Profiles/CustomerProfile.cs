using AutoMapper;
using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<CustomerUpdateDto, CustomerDto>();
            CreateMap<CustomerDto, CustomerUpdateDto>();
        }
    }
}
