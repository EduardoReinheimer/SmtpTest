using SendMail.Entities.Mail;

namespace SendMail.Dependencies;

public static class FluentEmailExtensions
{
    /// <summary>
    /// Extensión creada para añadir un servicio de FluentEmail con la configuración de appsettings.json
    /// </summary>
    /// <param name="services">Servicios de tu API</param>
    /// <param name="configuration">Configuraciones de tu API</param>
    /// <param name="secureConnection">Si activo, tenéis que agregar user y password en appsettings.json</param>
    /// <returns></returns>
    public static IServiceCollection AddFluentEmail(this IServiceCollection services,
       IConfiguration configuration, bool secureConnection = false)
    {
        var emailSettings = configuration.GetSection("EmailSettings");
        var defaultFromEmail = emailSettings["DefaultFromEmail"];
        var host = emailSettings["SMTPSetting:Host"];
        var port = emailSettings.GetValue<int>("SMTPSetting:Port");
        var userName = emailSettings?["SMTPSetting:UserName"];
        var password = emailSettings?["SMTPSetting:Password"];

        if (secureConnection)
        {
            services.AddFluentEmail(defaultFromEmail)
                .AddSmtpSender(host, port, userName, password);
        }
        else
        {
            services.AddFluentEmail(defaultFromEmail)
                .AddSmtpSender(host, port)
                .AddRazorRenderer();
        }
        return services;
    }
}
