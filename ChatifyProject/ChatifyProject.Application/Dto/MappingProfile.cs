using AutoMapper;
using ChatifyProject.Application.Model;

namespace ChatifyProject.Application.Dto
{
    public class MappingProfile : Profile  // using AutoMapper;
    {
        public MappingProfile()
        {
            CreateMap<ProfileDto, Profile>();
            CreateMap<Profile, ProfileDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
