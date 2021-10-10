using NetCoreBoilerplate.Application.Common.Mediator;

namespace NetCoreBoilerplate.Application.User.RegisterUser
{
    public class RegisterUserCommand : ICommand<VoidResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
