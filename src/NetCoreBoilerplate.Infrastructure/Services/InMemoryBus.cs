using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Domain.Common;

namespace NetCoreBoilerplate.Infrastructure.Services
{
    public class InMemoryBus : Application.Common.Interfaces.Services.IMediator
    {
        private readonly MediatR.IMediator _mediator;

        public InMemoryBus(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Send(IRequest request, CancellationToken ct = default(CancellationToken))
        {
            await _mediator.Send(request, ct);
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default(CancellationToken))
        {
            return await _mediator.Send(request, ct);
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            var notification = (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);

            await _mediator.Publish(notification);
        }
    }
}
