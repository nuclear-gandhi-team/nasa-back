using Microsoft.AspNetCore.Mvc;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO.Mail;

namespace Nasa.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailController: ControllerBase
{
    private readonly IMailService _mailService;

    public MailController(IMailService mailService)
    {
        _mailService = mailService;
    }

    [HttpPost]
    public async Task<ActionResult> SendMail(MailRequest mailRequest)
    {
        await _mailService.SendMailAsync(mailRequest);
        return Ok();
    }
}