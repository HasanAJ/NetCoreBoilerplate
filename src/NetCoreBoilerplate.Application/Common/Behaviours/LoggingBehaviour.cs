using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;

namespace NetCoreBoilerplate.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly IContext _context;

        public LoggingBehaviour(
            ILogger<TRequest> logger,
            IContext context)
        {
            _timer = new Stopwatch();

            _logger = logger;
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken ct, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();
            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            var userId = _context.UserId ?? string.Empty;
            _logger.LogInformation("[Request Complete] {Name} ({ElapsedMilliseconds} milliseconds) {@UserId}",
                typeof(TRequest).Name, elapsedMilliseconds, userId);

            return response;
        }
    }
}
