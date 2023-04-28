using AutoMapper;
using SimpleUser.API.DTOs;
using SimpleUser.Domain.Entities;

namespace SimpleCustomer.API.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<CustomerDto, Customer>()
                .ForMember(
                    dest => dest.CustomerDetail,
                    opt => opt.MapFrom(src => src.CustomerDetailDto)
                );
            CreateMap<Customer, CustomerDto>()
                .ForMember(
                    dest => dest.CustomerDetailDto,
                    opt => opt.MapFrom(src => src.CustomerDetail)
                );
            CreateMap<CustomerCreateDto, Customer>()
               .ForMember(
                   dest => dest.CustomerDetail,
                   opt => opt.MapFrom(src => src.CustomerDetailDto)
               );
            CreateMap<Customer, CustomerCreateDto>()
                .ForMember(
                    dest => dest.CustomerDetailDto,
                    opt => opt.MapFrom(src => src.CustomerDetail)
                );
            CreateMap<CustomerUpdateDto, Customer>()
               .ForMember(
                   dest => dest.CustomerDetail,
                   opt => opt.MapFrom(src => src.CustomerDetailDto)
               ).ForMember(
                   dest => dest.Password,
                   opt => {
                       opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.NewPassword));
                       opt.Condition(src => !string.IsNullOrEmpty(src.OldPassword) && !string.IsNullOrEmpty(src.NewPassword));
                   }
               );
            CreateMap<Customer, CustomerUpdateDto>()
                .ForMember(
                    dest => dest.CustomerDetailDto,
                    opt => opt.MapFrom(src => src.CustomerDetail)
                );
            CreateMap<CustomerDetailDto, CustomerDetail>();
            CreateMap<CustomerDetail, CustomerDetailDto>();
        }
    }
}
