using System;
using System.Collections.Generic;
using NetCoreBoilerplate.Domain.Common;
using NetCoreBoilerplate.Domain.Enums;

namespace NetCoreBoilerplate.Domain.Entities
{
    public class Account : Entity, IHasDomainEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AcceptTerms { get; set; }
        public Role Role { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public bool IsVerified => VerifiedOn.HasValue || LastPasswordResetOn.HasValue;
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpiresOn { get; set; }
        public DateTime? LastPasswordResetOn { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
