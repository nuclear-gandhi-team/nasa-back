using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Nasa.BLL
{
    public static class DependencyExtentions
    {
        public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly);
        }
    }
}

