using MediatR;

namespace NetCoreBoilerplate.Application.Common.Mediator
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}
