using TemplatesLibrary.Entities.Notification;

namespace SendMail.Entities.Mail;

public record RequestSendMailTemplate
{
    public string Asunto { get; init; }
    public string EmailDestino { get; init; }
    public List<string> EmailCopia { get; init; }
    public DefaultNotification Template { get; init; }

    public RequestSendMailTemplate(string asunto, string emailDestino, List<string> emailCopia, DefaultNotification template)
    {
        Asunto = asunto;
        EmailDestino = emailDestino;
        EmailCopia = emailCopia;
        Template = template;
    }
}