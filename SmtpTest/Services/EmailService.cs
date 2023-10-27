using SendMail.Entities.Mail;
using SendMail.Interfaces;
using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace SendMail.Services;

public class EmailService : IEmailService
{
    private IFluentEmail _fluentEmail;

    public EmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail ?? throw new ArgumentNullException(nameof(fluentEmail));
    }

    public async Task<SendResponse> SendMailAsync(RequestSendMail mailRequest)
    {
        try
        {
            return await _fluentEmail.To(mailRequest.EmailDestino)
            .Subject(mailRequest.Asunto)
            .Body(mailRequest.Body, mailRequest.EsHtml)
            .SendAsync();
        }
        catch (Exception ex)
        {
            var responseError = new SendResponse();
            responseError.ErrorMessages.Add(ex.Message);
            return responseError;
        }
    }

    public async Task<bool> SendTestMail()
    {
        bool success = true;
        try
        {
            await _fluentEmail.To("mail@example.com").Body("The body").SendAsync();
        }
        catch (Exception)
        {
            success = false;
        }
        return success;
    }


}
