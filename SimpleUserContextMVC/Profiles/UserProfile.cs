using AutoMapper;
using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserUpdateDto, UserDto>();
            CreateMap<UserDto, UserUpdateDto>();
        }
    }
}
