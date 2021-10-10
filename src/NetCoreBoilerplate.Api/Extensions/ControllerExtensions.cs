using System.Text.Json;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using NetCoreBoilerplate.Api.Filters;

namespace NetCoreBoilerplate.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                    options.Filters.Add<ApiExceptionFilter>()
                )
                .AddFluentValidation()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.IgnoreNullValues = true;
                    x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            return services;
        }
    }
}
