using AutoMapper;
using Nasa.Common.DTO;
using Nasa.Common.DTO.User;
using Nasa.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.MappingProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, AuthorizationResponse>().ReverseMap();
            CreateMap<RegisterUserDto, User>().ReverseMap();
            CreateMap<RegisterUserDto, LoginUserDto>().ReverseMap();
            CreateMap<LoginUserDto, User>().ReverseMap();
        }        
    }
}
