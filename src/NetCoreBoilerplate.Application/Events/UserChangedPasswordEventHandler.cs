using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Constants;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Application.Common.Models;
using NetCoreBoilerplate.Domain.Events.User;

namespace NetCoreBoilerplate.Application.Events
{
    public class UserChangedPasswordEventHandler : INotificationHandler<DomainEventNotification<UserChangedPasswordEvent>>
    {
        private readonly IMailService _mailService;

        public UserChangedPasswordEventHandler(IMailService mailService)
        {
            _mailService = mailService;
        }

        public Task Handle(DomainEventNotification<UserChangedPasswordEvent> notification, CancellationToken ct)
        {
            var domainEvent = notification.DomainEvent;

            var email = new Email<object>()
            {
                To = domainEvent.Email,
                Subject = "NetCoreBoilerplate - Password Changed",
                Template = EmailTemplates.CHANGE_PASSWORD
            };

            Task.Run(async () => await _mailService.Send(email));

            return Task.CompletedTask;
        }
    }
}
