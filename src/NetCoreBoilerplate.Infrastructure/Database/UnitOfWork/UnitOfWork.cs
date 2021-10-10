using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NetCoreBoilerplate.Application.Common.Interfaces.Database;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Domain.Common;
using NetCoreBoilerplate.Infrastructure.Common.Database;
using NetCoreBoilerplate.Infrastructure.Common.Extensions;
using NetCoreBoilerplate.Infrastructure.Database.Context;

namespace NetCoreBoilerplate.Infrastructure.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly IMediator _domainEventService;
        private readonly IDateTime _dateTime;
        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext db,
            IMediator domainEventService,
            IDateTime dateTime)
        {
            _db = db;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        public IRepository<T> GetRepository<T>() where T : Entity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _db);
                _repositories.Add(type, repositoryInstance);
            }

            return (Repository<T>)_repositories[type];
        }

        public T GetCustomRepository<T>() where T : class
        {
            if (!typeof(T).IsInterface)
            {
                throw new ArgumentException("Generic type should be an interface.");
            }

            return _db.GetService<T>();
        }

        public async Task<int> SaveChanges(CancellationToken ct)
        {
            foreach (EntityEntry<Entity> entry in _db.ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTime.Now;
                        break;
                }
            }

            int result = await _db.SaveChangesAsync(ct);

            await _domainEventService.PublishDomainEvents(_db);

            return result;
        }

        public async Task Dispose()
        {
            await _db.DisposeAsync();
        }
    }
}
