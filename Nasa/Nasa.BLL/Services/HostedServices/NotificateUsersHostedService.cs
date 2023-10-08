using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO.Mail;
using Nasa.DAL.Entities;


namespace Nasa.BLL.Services.HostedServices
{
    public class NotificateUsersHostedService : IHostedService, IDisposable
    {
        private Timer? _timer = null;        
        private readonly IServiceProvider _serviceProvider;
        private const double MAX_DISTANCE = 2;

        public NotificateUsersHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(NotificateUsersAboutCurrentFires, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        private async void NotificateUsersAboutCurrentFires(object? state)
        {
            var scope = _serviceProvider.CreateScope();

            var mailService = scope.ServiceProvider.GetService<IMailService>()!;
            var currentFiresService = scope.ServiceProvider.GetService<ICurrentFiresService>()!;
            var subscribeService = scope.ServiceProvider.GetService<ISubscribeService>()!;
            var coordinatesService = scope.ServiceProvider.GetService<ICoordinatesService>()!;
            var logger = scope.ServiceProvider.GetService<ILogger<NotificateUsersHostedService>>()!;

            var subscriptions = await subscribeService.GetAllSubscriptions();
            var fires = await currentFiresService.GetCurrentFires(DateTime.Now, 1);

            foreach (var subscription in subscriptions)
            {
                foreach (var fire in fires)
                {
                    try
                    {
                        if (coordinatesService.ComputeDistance(fire, coordinatesService.GetCoordinatesInstance(subscription.Coordinates)) < MAX_DISTANCE)
                        {
                            await mailService.SendMailAsync(new MailRequest { ToEmail = subscription.UserEmail, Subject = "Fire near you!" });
                        }
                    }
                    catch(ArgumentException ex)
                    {
                        logger.LogError(ex.Message);
                    }   
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
