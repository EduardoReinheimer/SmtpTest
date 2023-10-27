using FluentEmail.Core.Models;
using SendMail.Entities.Mail;

namespace SendMail.Interfaces;

public interface IEmailService
{
    Task<bool> SendTestMail();

    Task<SendResponse> SendMailAsync(RequestSendMail mailRequest);
}
