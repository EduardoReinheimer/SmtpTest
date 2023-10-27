namespace SendMail.Entities.Mail;

public record RequestSendMail
{
    public string Asunto { get; init; }
    public bool EsHtml { get; init; }
    public string EmailDestino { get; init; }
    public List<string> EmailCopia { get; init; }
    public string Body { get; init; }

    public RequestSendMail(
        string asunto,
        bool esHtml,
        string emailDestino,
        List<string> emailCopia,
        string body)
    {
        Asunto = asunto;
        EsHtml = esHtml;
        EmailDestino = emailDestino;
        EmailCopia = emailCopia;
        Body = body;
    }
}
