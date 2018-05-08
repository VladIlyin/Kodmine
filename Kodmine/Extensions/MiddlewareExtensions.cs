using Kodmine.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodmine.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseDynamicLayoutMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DynamicLayoutMiddleware>();
        }
    }
}
