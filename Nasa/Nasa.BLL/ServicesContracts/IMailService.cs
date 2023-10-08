using Nasa.Common.DTO.Mail;

namespace Nasa.BLL.ServicesContracts;

public interface IMailService
{
    Task SendMailAsync(MailRequest mailRequest);
}