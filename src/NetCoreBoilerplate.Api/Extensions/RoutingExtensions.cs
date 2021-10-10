using Microsoft.Extensions.DependencyInjection;

namespace NetCoreBoilerplate.Api.Extensions
{
    public static class RoutingExtensions
    {
        public static IServiceCollection AddCustomRouting(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            return services;
        }
    }
}
