using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ValidationException = NetCoreBoilerplate.Application.Common.Exceptions.ValidationException;

namespace NetCoreBoilerplate.Api.Extensions
{
    public static class ApiBehaviorExtensions
    {
        public static IServiceCollection ConfigureApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState);
                    throw new ValidationException(problemDetails.Errors);
                };
            });

            return services;
        }
    }
}
