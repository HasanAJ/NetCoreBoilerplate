using MediatR;

namespace NetCoreBoilerplate.Application.Common.Mediator
{
    public interface IEventHandler<in TNotification> :
        INotificationHandler<TNotification> where TNotification : INotification
    {

    }
}
