using AutoMapper;
using SimpleUser.Domain.Entities;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Profiles
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
            CreateMap<UserCreateDto, User>()
               .ForMember(
                   dest => dest.UserDetail,
                   opt => opt.MapFrom(src => src.UserDetailDto)
               );
            CreateMap<User, UserCreateDto>()
                .ForMember(
                    dest => dest.UserDetailDto,
                    opt => opt.MapFrom(src => src.UserDetail)
                );
            CreateMap<UserUpdateDto, User>()
               .ForMember(
                   dest => dest.UserDetail,
                   opt => opt.MapFrom(src => src.UserDetailDto)
               );
            CreateMap<User, UserUpdateDto>()
                .ForMember(
                    dest => dest.UserDetailDto,
                    opt => opt.MapFrom(src => src.UserDetail)
                );
            CreateMap<UserDetailDto, UserDetail>();
            CreateMap<UserDetail, UserDetailDto>();
        }
    }
}
