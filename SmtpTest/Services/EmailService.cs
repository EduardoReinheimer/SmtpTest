using SendMail.Entities.Mail;
using SendMail.Interfaces;
using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace SendMail.Services;

public class EmailService : IEmailService
{
    private IFluentEmail _fluentEmail;
    private readonly IFluentEmailFactory _fluentEmailFactory;

    public EmailService(IFluentEmail fluentEmail, IFluentEmailFactory fluentEmailFactory)
    {
        _fluentEmail = fluentEmail ?? throw new ArgumentNullException(nameof(fluentEmail));
        _fluentEmailFactory = fluentEmailFactory ?? throw new ArgumentNullException(nameof(fluentEmailFactory));
    }

    public async Task<SendResponse> SendMailAsync(RequestSendMail mailRequest)
    {
        try
        {
            return await _fluentEmail.To(mailRequest.EmailDestino)
            .Subject(mailRequest.Asunto)
            .Body(mailRequest.Body, true)
            .SendAsync();
        }
        catch (Exception ex)
        {
            var responseError = new SendResponse();
            responseError.ErrorMessages.Add(ex.Message);
            return responseError;
        }
    }

    public async Task<SendResponse> SendMailNotificationAsync(RequestSendMailTemplate mailRequest)
    {
        try
        {
            var path = Path.GetFullPath("Templates\\DefaultNotification.cshtml");

            return await _fluentEmail.To(mailRequest.EmailDestino)
            .Subject(mailRequest.Asunto)
            .UsingTemplateFromFile(path, mailRequest.Template)
            .SendAsync();
        }
        catch (Exception ex)
        {
            var responseError = new SendResponse();
            responseError.ErrorMessages.Add(ex.Message);
            return responseError;
        }
    }

    public async Task<List<SendResponse>> SendMultipleMailNotificationAsync(List<RequestSendMailTemplate> mailRequests)
    {
        var response = new List<SendResponse>();

        var path = Path.GetFullPath("Templates\\DefaultNotification.cshtml");

        var tasks = new List<Task<SendResponse>>();
        foreach (var mail in mailRequests)
        {
            tasks.Add(Task.Run(async () => await SendMailNotificationAsync(mail)));
        }

        await Task.WhenAll(tasks);

        //Así que listo vamos poner todos los retornos de error
        tasks.ForEach(task =>
        {
            response.Add(task.Result);
        });

        return response;
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
