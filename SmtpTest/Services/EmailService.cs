using SendMail.Entities.Mail;
using SendMail.Interfaces;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using System.Text.Json.Serialization;
using TemplatesLibrary.Enums;

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
            //Borar eso, es experimental, está mal poner path asi
            var path = Path.GetFullPath("../TemplatesLibrary");
            var fullpath = Path.Combine(path, Modulo.Templates.ToString(), Accion.Notification.ToString(), TemplateFile.DefaultNotification.ToString());
            var fullPathNameWithExtension = Path.ChangeExtension(fullpath, "cshtml");
            var listFields = new List<CampoValor>();
            var field = new CampoValor("Nombre", "Valor");
            listFields.Add(field);
            var notification = new DefaultNotification(
                    titulo: "Correo",
                    descripcionCorta: "descripcion",
                    camposDinamicos: listFields
                );

             path = @"C:\Users\eduardo.reinheimer\source\repos\SmtpTest\TemplatesLibrary\Templates\Notification\DefaultNotification.cshtml";

            return await _fluentEmail.To(mailRequest.EmailDestino)
            .Subject(mailRequest.Asunto)
            .UsingTemplateFromFile(path, notification)
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
            
            return await _fluentEmail.To(mailRequest.EmailDestino)
            .Subject(mailRequest.Asunto)
            .UsingTemplateFromFile("", mailRequest.Template)
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
