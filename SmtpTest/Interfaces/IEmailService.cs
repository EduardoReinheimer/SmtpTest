using FluentEmail.Core.Models;
using SendMail.Entities.Mail;

namespace SendMail.Interfaces;

public interface IEmailService
{

    Task<SendResponse> SendMailAsync(RequestSendMail mailRequest);
    Task<SendResponse> SendMailNotificationAsync(RequestSendMailTemplate mailRequest);
    Task<List<SendResponse>> SendMultipleMailNotificationAsync(List<RequestSendMailTemplate> mailRequest);
}
