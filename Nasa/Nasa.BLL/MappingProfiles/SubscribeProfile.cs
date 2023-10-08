using AutoMapper;
using Nasa.Common.DTO.Subscribe;
using Nasa.DAL.Entities;

namespace Nasa.BLL.MappingProfiles;

public class SubscribeProfile: Profile
{
    public SubscribeProfile()
    {
        CreateMap<Subscription, SubscribeDto>()
            .ForMember(s => s.UserEmail, s => s.MapFrom(x => x.User.Email ))
            .ReverseMap();
    }
}