using Nasa.Common.DTO.Subscribe;

namespace Nasa.BLL.ServicesContracts;

public interface ISubscribeService
{
    Task SubscribeAsync(SubscribeDto subscribeDto);
    Task UnsubscribeAsync(SubscribeDto subscribeDto);
}