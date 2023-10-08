using AutoMapper;
using Nasa.Common.DTO;
using Nasa.Common.DTO.User;
using Nasa.DAL.Entities;

namespace Nasa.BLL.MappingProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, AuthorizationResponse>()
                .ForMember(r => r.UserDto, r => r.MapFrom(x => new UserDto { Email = x.Email, Username = x.Username, Coordinates = x.Сoordinates}))
                .ReverseMap();
            CreateMap<RegisterUserDto, User>().ReverseMap();
            CreateMap<RegisterUserDto, LoginUserDto>().ReverseMap();
            CreateMap<LoginUserDto, User>().ReverseMap();
        }        
    }
}
