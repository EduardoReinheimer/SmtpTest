namespace SendMail;

public record DefaultNotification
{
    public string Titulo { get; init; }
    public string DescripcionCorta { get; init; }
    public List<CampoValor> CamposDinamicos { get; init; }
    public DefaultNotification(string titulo, string descripcionCorta, List<CampoValor> camposDinamicos)
    {
        Titulo = titulo;
        DescripcionCorta = descripcionCorta;
        CamposDinamicos = camposDinamicos;
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
