using MediatR;

namespace NetCoreBoilerplate.Application.Common.Mediator
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
