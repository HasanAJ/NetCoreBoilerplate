using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Constants;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Application.Common.Models;
using NetCoreBoilerplate.Domain.Events.User;

namespace NetCoreBoilerplate.Application.Events
{
    public class UserCreatedEventHandler : IEventHandler<DomainEventNotification<UserCreatedEvent>>
    {
        private readonly IMailService _mailService;

        public UserCreatedEventHandler(IMailService mailService)
        {
            _mailService = mailService;
        }

        public Task Handle(DomainEventNotification<UserCreatedEvent> notification, CancellationToken ct = default(CancellationToken))
        {
            var domainEvent = notification.DomainEvent;

            // TODO: update template with link to confirm email
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

            // TODO: implement a background job handler
            Task.Run(async () => await _mailService.Send(email), ct);

            return Task.CompletedTask;
        }
    }
}
