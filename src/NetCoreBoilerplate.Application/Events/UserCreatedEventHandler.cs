using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Constants;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Application.Common.Models;
using NetCoreBoilerplate.Domain.Events.User;

namespace NetCoreBoilerplate.Application.Events
{
    public class UserCreatedEventHandler : INotificationHandler<DomainEventNotification<UserCreatedEvent>>
    {
        private readonly IMailService _mailService;

        public UserCreatedEventHandler(IMailService mailService)
        {
            _mailService = mailService;
        }

        public Task Handle(DomainEventNotification<UserCreatedEvent> notification, CancellationToken ct)
        {
            var domainEvent = notification.DomainEvent;

            var email = new Email<object>()
            {
                To = domainEvent.Email,
                Subject = "NetCoreBoilerplate - Welcome",
                Template = EmailTemplates.SIGN_UP,
                Model = new
                {
                    Email = domainEvent.Email
                }
            };

            Task.Run(async () => await _mailService.Send(email));

            return Task.CompletedTask;
        }
    }
}
