using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreBoilerplate.Application.Common.Config;

namespace NetCoreBoilerplate.Api.Extensions
{
    public static class ConfigExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection("Jwt"));
            services.Configure<SmtpConfig>(configuration.GetSection("Smtp"));

            return services;
        }
    }
}
