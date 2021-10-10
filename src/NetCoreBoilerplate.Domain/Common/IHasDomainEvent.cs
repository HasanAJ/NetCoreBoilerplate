using System.Collections.Generic;

namespace NetCoreBoilerplate.Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
}
