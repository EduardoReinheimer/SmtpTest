namespace SendMail.Entities.Mail;

public record RequestSendMail
{
    public string Asunto { get; init; }
    public string EmailDestino { get; init; }
    public List<string> EmailCopia { get; init; }
    public string Body { get; init; }

    public RequestSendMail(
        string asunto,
        string emailDestino,
        List<string> emailCopia,
        string body)
    {
        Asunto = asunto;
        EmailDestino = emailDestino;
        EmailCopia = emailCopia;
        Body = body;
    }
}
