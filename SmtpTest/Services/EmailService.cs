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
            var addressesList = new List<Address>();
            addressesList.Clear();
            return await _fluentEmail.To(mailRequest.EmailDestino)
                .Subject(mailRequest.Asunto)
                .CC(addressesList)
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

            var addressesList = new List<Address>();
            addressesList.Clear();
            mailRequest.EmailCopia.ForEach(ccmail => addressesList.Add(new Address(ccmail)));

            return await _fluentEmail.To(mailRequest.EmailDestino)
                .Subject(mailRequest.Asunto)
                .CC(addressesList)
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
        var path = Path.GetFullPath("Templates\\DefaultNotification.cshtml");

        var addressesList = new List<Address>();
        var tasks = new List<Task<SendResponse>>();
        foreach (var mail in mailRequests)
        {
            addressesList.Clear();
            mail.EmailCopia.ForEach(ccmail => addressesList.Add(new Address(ccmail)));

            tasks.Add(Task.Run(async () => await _fluentEmailFactory
                .Create()
                .To(mail.EmailDestino)
                .CC(addressesList)
                .Subject(mail.Asunto)
                .UsingTemplateFromFile(path, mail.Template)
                .SendAsync()));
        }
        await Task.WhenAll(tasks);
        var result = new List<SendResponse>();
        tasks.ForEach(tsk =>
        {
            result.Add(tsk.Result);
        });

        return result;
    }
}