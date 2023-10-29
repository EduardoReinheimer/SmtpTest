namespace SendMail;

public record DefaultNotification
{
    public string Titulo { get; init; }
    public string DescripcionCorta { get; init; }
    public List<CampoValor> CamposDinamicos { get; init; }
    public string FechaEnvío { get; set; }
    public DefaultNotification(string titulo, string descripcionCorta, List<CampoValor> camposDinamicos)
    {
        Titulo = titulo;
        DescripcionCorta = descripcionCorta;
        CamposDinamicos = camposDinamicos;
        FechaEnvío = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    }
}

public record CampoValor
{
    public string FieldName { get; init; }
    public string FieldValue { get; init; }
    public CampoValor(string fieldName, string fieldValue)
    {
        FieldName = fieldName;
        FieldValue = fieldValue;
    }
}
