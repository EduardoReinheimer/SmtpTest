using RazorLight.Extensions;
using SendMail.Dependencies;
using SendMail.Interfaces;
using SendMail.Services;
using Serilog;

namespace SendMail;

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
        services.AddFluentEmail(Configuration);
        services.AddTransient<IEmailService, EmailService>();
    }

    public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.UseSerilogRequestLogging(opts
            => opts.EnrichDiagnosticContext = LogRequestEnricher.EnrichFromRequest);
    }

}