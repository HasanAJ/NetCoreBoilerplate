using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetCoreBoilerplate.Api.Filters;
using NetCoreBoilerplate.Api.Services;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Infrastructure.Database.Context;

namespace NetCoreBoilerplate.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, MigrationStartupFilter<ApplicationDbContext>>();
            services.AddSingleton<IContext, ContextService>();
            services.AddScoped<ApiExceptionFilter>();

            return services;
        }
    }
}
