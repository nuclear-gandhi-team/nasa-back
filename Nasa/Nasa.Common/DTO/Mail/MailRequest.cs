namespace Nasa.Common.DTO.Mail;

public class MailRequest
{
    public string ToEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
}