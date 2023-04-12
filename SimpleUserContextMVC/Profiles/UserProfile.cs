using AutoMapper;
using SimpleUser.Domain.Entities;
using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserDto, User>()
                .ForMember(
                    dest => dest.UserDetail,
                    opt => opt.MapFrom(src => src.UserDetailDto)
                );
            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest.UserDetailDto,
                    opt => opt.MapFrom(src => src.UserDetail)
                );
            CreateMap<UserDetailDto, UserDetail>();
            CreateMap<UserDetail, UserDetailDto>();
        }
    }
}
