using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO.Mail;

namespace Nasa.BLL.Services;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendMailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;

        var builder = new BodyBuilder
        {
            HtmlBody = GetBody()
        };
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    private string GetBody()
    {
        return
            @"<body style=""font-family: Arial, sans-serif; background-color: #ffcccc; text-align: center; padding: 20px;"">
            <h1 style=""color: #ff0000;"">Fire Nearby!</h1>
            
            <p>Attention! There is a fire nearby. Take immediate safety precautions.</p>
            
            <p>Thank you for your cooperation!</p>

            <div style=""margin-top: 20px;"">
                <a href=""link"" style=""text-decoration: none; background-color: #ff0000; color: #ffffff; padding: 10px 20px; margin-right: 10px; border-radius: 10px;"">Details</a>
                <a href=""link"" style=""text-decoration: none; background-color: #ff0000; color: #ffffff; padding: 10px 20px; border-radius: 10px;"">Unsubscribe</a>
            </div>
        </body>";
    }
}