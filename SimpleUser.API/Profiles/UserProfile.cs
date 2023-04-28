using AutoMapper;
using SimpleUser.API.DTOs;
using SimpleUser.Persistence.Data;

namespace SimpleUser.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SignUpDto, ApplicationUser>()
                .ForMember(
                    dest => dest.UserName,
                    otp => otp.MapFrom(src => src.Email)
                );
            CreateMap<ApplicationUser, SignUpDto>()
                .ForMember(
                    dest => dest.Email,
                    otp => otp.MapFrom(src => src.UserName)
                );
        }
    }
}
