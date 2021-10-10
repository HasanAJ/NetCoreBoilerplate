using MediatR;

namespace NetCoreBoilerplate.Application.Common.Mediator
{
    public interface INotificationHandler<in TNotification> where TNotification : INotification
    {

    }
}
