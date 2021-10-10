using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NetCoreBoilerplate.Application.Common.Exceptions;

namespace NetCoreBoilerplate.Api.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Handling exception");

            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            int statusCode = StatusCodes.Status500InternalServerError;
            string code = "SERVER_ERROR";
            var ex = context.Exception;

            IDictionary<string, string[]> errors = null;

            if (ex is ExceptionBase knownException)
            {
                statusCode = knownException.StatusCode;
                code = knownException.Code;
            }

            if (ex is ValidationException validationException)
            {
                errors = validationException.Errors;
            }

            var details = new
            {
                Code = code,
                Message = ex.Message,
                Errors = errors,
                // TODO: only output stacktrace if env is dev
                StackTrace = ex.ToString()
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
