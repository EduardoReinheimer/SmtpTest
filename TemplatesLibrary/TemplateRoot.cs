using TemplatesLibrary.Enums;

namespace TemplatesLibrary;

/// <summary>
/// Solo sirve para ubicar la carpeta
/// </summary>
public class TemplateRoot
{
    public string BasePath { get; }

    public TemplateRoot()
    {
        // Determina la ubicación base en función del directorio actual del ensamblado.
        // Esto asume que la clase se encuentra en el mismo proyecto que las plantillas.
        var assemblyLocation = this.GetType().Name;
        var basePath = Path.GetDirectoryName(assemblyLocation);

        // Utiliza los valores de los enumerados para construir la ruta
        BasePath = !string.IsNullOrEmpty(basePath) ? basePath : string.Empty;
    }

    public string GetTemplatePath(Modulo modulo, Accion accion, TemplateFile file)
    {
        // Combinar la ubicación base con el nombre de la plantilla
        return Path.Combine(BasePath, modulo.ToString(), accion.ToString(), file.ToString());
    }

    public string GetTemplatePath(string templateName)
    {
        // Combinar la ubicación base con el nombre de la plantilla
        return Path.Combine(BasePath, templateName);
    }
}
