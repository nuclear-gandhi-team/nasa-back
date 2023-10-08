using Microsoft.EntityFrameworkCore;
using Nasa.DAL.Context;

namespace Nasa.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseNasaContext(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        using var context = scope?.ServiceProvider.GetRequiredService<NasaContext>();
        context?.Database.Migrate();
    }
}