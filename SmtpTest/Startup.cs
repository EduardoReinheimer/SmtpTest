using Api.Interfaces;
using Api.Services;

namespace Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddFluentEmail("mail@example.com")
                .AddSmtpSender("localhost", 25);

        //Parte de injección de dependencias que debería estar en otro proyecto
        services.AddTransient<IEmailService, EmailService>();
    }

    public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env)
    {

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

}