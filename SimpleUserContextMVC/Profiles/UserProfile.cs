using AutoMapper;
using SimpleUserContext.Models;
using SimpleUserContextMVC.DTOs;

namespace SimpleUserContextMVC.Profiles
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
