using AutoMapper;
using Nasa.Common.DTO.Subscribe;
using Nasa.DAL.Entities;

namespace Nasa.BLL.MappingProfiles;

public class SubscribeProfile: Profile
{
    public SubscribeProfile()
    {
        CreateMap<Subscription, SubscribeDto>().ReverseMap();
    }
}