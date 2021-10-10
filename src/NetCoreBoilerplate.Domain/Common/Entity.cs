using System;

namespace NetCoreBoilerplate.Domain.Common
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
