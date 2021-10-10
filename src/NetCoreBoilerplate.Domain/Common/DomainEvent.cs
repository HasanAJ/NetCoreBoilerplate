using System;

namespace NetCoreBoilerplate.Domain.Common
{
    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; protected set; } = DateTime.UtcNow;
    }
}
