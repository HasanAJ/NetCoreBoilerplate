using System.Linq;
using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Domain.Common;
using NetCoreBoilerplate.Infrastructure.Database.Context;

namespace NetCoreBoilerplate.Infrastructure.Common.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task PublishDomainEvents<T>(this IMediator domainEventService, T db) where T : ApplicationDbContext
        {
            var domainEntities = db.ChangeTracker
                .Entries<IHasDomainEvent>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.DomainEvents.Clear());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await domainEventService.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
