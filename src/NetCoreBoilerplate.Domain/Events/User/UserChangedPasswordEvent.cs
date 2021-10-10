using NetCoreBoilerplate.Domain.Common;

namespace NetCoreBoilerplate.Domain.Events.User
{
    public class UserChangedPasswordEvent : DomainEvent
    {
        public UserChangedPasswordEvent(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
