using Nasa.API.Extensions;
using Nasa.BLL.Services;
using Nasa.BLL.Services.JWT;
using Nasa.BLL.ServicesContracts;
using System.Reflection;

namespace Nasa.API;

public class Startup
{
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddNasaContext(_configuration);
        
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddControllers();
        services.AddAutoMapper(Assembly);
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
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