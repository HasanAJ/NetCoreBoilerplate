using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NetCoreBoilerplate.Domain.Common;
using NetCoreBoilerplate.Domain.Entities;

namespace NetCoreBoilerplate.Infrastructure.Database.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(IHasDomainEvent).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType).Ignore(nameof(IHasDomainEvent.DomainEvents));
                }
            }

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
