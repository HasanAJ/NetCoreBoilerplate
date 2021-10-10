using Microsoft.AspNetCore.Builder;
using NetCoreBoilerplate.Api.Middleware;

namespace NetCoreBoilerplate.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<AccountMiddleware>();

            return app;
        }
    }
}
