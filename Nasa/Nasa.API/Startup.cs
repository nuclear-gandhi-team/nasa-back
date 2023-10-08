using Nasa.API.Extensions;
using Nasa.API.Filter;
using Nasa.API.Middlewares;
using Nasa.BLL;
using Nasa.BLL.Services;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO.Mail;

namespace Nasa.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddNasaContext(_configuration);
        services.ConfigureJwt(_configuration);
        services.AddMapper();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddControllers(o =>
        {
            o.Filters.Add(typeof(CustomExceptionFilter));
        });
        services.AddCustomServices();
        
        services.Configure<MailSettings>(_configuration.GetSection("MailSettings"));
        services.AddTransient<IMailService, MailService>();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

        app.UseRouting();
        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<CurrentUserMiddleware>();

        app.UseEndpoints(cfg =>
        {
            cfg.MapControllers();
        });
    }
}