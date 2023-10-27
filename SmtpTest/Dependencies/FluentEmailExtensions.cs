namespace SendMail.Dependencies;

public static class FluentEmailExtensions
{
    public static IServiceCollection AddFluentEmail(this IServiceCollection services,
       IConfiguration configuration)
    {
        var emailSettings = configuration.GetSection("EmailSettings");
        var defaultFromEmail = emailSettings["DefaultFromEmail"];
        var host = emailSettings["SMTPSetting:Host"];
        var port = emailSettings.GetValue<int>("Port");
        var userName = emailSettings["UserName"];
        var password = emailSettings["Password"];
        services.AddFluentEmail(defaultFromEmail)
            .AddSmtpSender(host, port, userName, password);
        return services;
    }
}
