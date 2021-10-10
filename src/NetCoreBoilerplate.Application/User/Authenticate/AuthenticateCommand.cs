using NetCoreBoilerplate.Application.Common.Mediator;

namespace NetCoreBoilerplate.Application.User.Authenticate
{
    public class AuthenticateCommand : ICommand<TokenResposeDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
