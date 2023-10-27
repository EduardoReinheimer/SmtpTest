using Api.Entities.Mail;
using FluentEmail.Core.Models;

namespace Api.Interfaces;

public interface IEmailService
{
    Task<bool> SendTestMail();

    Task<SendResponse> SendMailAsync(RequestSendMail mailRequest);
}
