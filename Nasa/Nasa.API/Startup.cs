using Nasa.API.Extensions;
using Nasa.BLL;
using Nasa.BLL.Services;
using Nasa.BLL.Services.JWT;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.Auth;
using System.Reflection;

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

        services.AddControllers();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<JwtIssuerOptions>();
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

        app.UseEndpoints(cfg =>
        {
            cfg.MapControllers();
        });
    }
}