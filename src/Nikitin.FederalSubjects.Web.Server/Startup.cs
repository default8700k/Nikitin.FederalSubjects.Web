using HealthChecks.UI.Client;
using Maltsev.RequestRedirector;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Nikitin.FederalSubjects.Web.Server.Models;

namespace Nikitin.FederalSubjects.Web.Server;

public class Startup
{
    private readonly ConfigurationModel _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration.Get<ConfigurationModel>();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

        services.AddControllers().AddNewtonsoftJson();

        services.AddHttpClient("External", httpClient =>
        {
            httpClient.BaseAddress = new Uri(_configuration.ExternalService.BaseAddress);
            httpClient.Timeout = _configuration.ExternalService.Timeout;
        });

        services.AddSwaggerGenNewtonsoftSupport();
        services.AddSwaggerGen(x =>
        {
            x.EnableAnnotations();
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "Nikitin.FederalSubjects.Web.Server", Version = "v1" });
        });

        services.AddHealthChecks();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment() == true)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Nikitin.FederalSubjects.Web.Server v1"));
        }
        else
        {
            app.UseHsts();
        }

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.Map("/api", x =>
            x.UseRequestRedirector("External")
        );

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");

            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            endpoints.MapHealthChecks("/health/lite", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                Predicate = (check) => !check.Tags.Contains("deep")
            });
        });
    }
}
