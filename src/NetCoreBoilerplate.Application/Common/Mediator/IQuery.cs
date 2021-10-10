using MediatR;

namespace NetCoreBoilerplate.Application.Common.Mediator
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}
