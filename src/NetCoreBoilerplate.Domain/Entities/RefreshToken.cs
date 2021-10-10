using System;
using NetCoreBoilerplate.Domain.Common;

namespace NetCoreBoilerplate.Domain.Entities
{
    public class RefreshToken : Entity
    {
        public int AccountId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public bool IsActive => !IsRevoked && !IsExpired;

        public virtual Account Account { get; set; }
    }
}
