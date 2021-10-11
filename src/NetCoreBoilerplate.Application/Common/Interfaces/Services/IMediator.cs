using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetCoreBoilerplate.Domain.Common;

namespace NetCoreBoilerplate.Application.Common.Interfaces.Services
{
    public interface IMediator
    {
        Task Send(IRequest request, CancellationToken ct = default(CancellationToken));

        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default(CancellationToken));

        Task Publish(DomainEvent domainEvent);
    }
}
