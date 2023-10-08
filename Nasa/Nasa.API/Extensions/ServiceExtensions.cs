using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nasa.Common.Auth;
using Nasa.DAL.Context;
using System.Text;
using Nasa.BLL.Services;
using Nasa.BLL.Services.JWT;
using Nasa.BLL.ServicesContracts;
using Nasa.BLL.Services.CurrentFires;

namespace Nasa.API.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICurrentFiresService, CurrentFiresService>();
        services.AddScoped<JwtIssuerOptions>();
        
        services.AddScoped<UserStorageService>();
        services.AddTransient<IUserIdGetter>(s => s.GetRequiredService<UserStorageService>());
        services.AddTransient<IUserIdSetter>(s => s.GetRequiredService<UserStorageService>());

        services.AddScoped<ISubscribeService, SubscribeService>();
    }
    public static void AddNasaContext(this IServiceCollection services, IConfiguration configuration)
    {
        var squirrelCoreDbConnectionString = "NasaConnectionString";
        var connectionsString = configuration.GetConnectionString(squirrelCoreDbConnectionString);
        services.AddDbContext<NasaContext>(options =>
            options.UseSqlServer(
                connectionsString,
                opt => opt.MigrationsAssembly(typeof(NasaContext).Assembly.GetName().Name)));
    }

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var secretKey = configuration["Jwt:Key"];
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

        var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

        // Configure JwtIssuerOptions (test)
        services.Configure<JwtIssuerOptions>(options =>
        {
            options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
            options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}