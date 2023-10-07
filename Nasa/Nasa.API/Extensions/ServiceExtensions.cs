using Microsoft.EntityFrameworkCore;
using Nasa.DAL.Context;

namespace Nasa.API.Extensions;

public static class ServiceExtensions
{
    public static void AddNasaContext(this IServiceCollection services, IConfiguration configuration)
    {
        var squirrelCoreDbConnectionString = "NasaConnectionString";
        var connectionsString = configuration.GetConnectionString(squirrelCoreDbConnectionString);
        services.AddDbContext<NasaContext>(options =>
            options.UseSqlServer(
                connectionsString,
                opt => opt.MigrationsAssembly(typeof(NasaContext).Assembly.GetName().Name)));
    }
}