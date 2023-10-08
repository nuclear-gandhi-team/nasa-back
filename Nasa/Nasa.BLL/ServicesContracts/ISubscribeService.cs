using Nasa.Common.DTO.Subscribe;
using Nasa.DAL.Entities;

namespace Nasa.BLL.ServicesContracts;

public interface ISubscribeService
{
    Task<List<SubscribeDto>> GetAllSubscription();
    Task SubscribeAsync(SubscribeDto subscribeDto);
    Task UnsubscribeAsync(SubscribeDto subscribeDto);
}