using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

