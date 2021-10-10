using NetCoreBoilerplate.Domain.Common;

namespace NetCoreBoilerplate.Domain.Events.User
{
    public class UserCreatedEvent : DomainEvent
    {
        public UserCreatedEvent(string email, string verificationToken)
        {
            Email = email;
            VerificationToken = verificationToken;
        }

        public string Email { get; }
        public string VerificationToken { get; }
    }
}
