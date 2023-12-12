using System.Text.Json.Serialization;
using Web.Extensions;
using Web.Middlewares;

namespace Web;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddControllers()
                .AddJsonOptions
                    (options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    });

        services.AddSingleton<GlobalExceptionHandlingMiddleware>();
        services.AddCustomDbContext(Configuration);
        services.AddAndConfigureHostedServices();
        services.AddAndConfigureAuthentication(Configuration);
        services.AddAndConfigureAuthorization();
        services.AddMediatRServices();
        services.AddAndConfigureOptions(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.UseEndpoints
            (endpoints =>
            {
                endpoints.MapControllers();
            });
    }
}