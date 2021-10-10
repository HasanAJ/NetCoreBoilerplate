using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Application.User.Authenticate;

namespace NetCoreBoilerplate.Application.User.RefreshToken
{
    public class RefreshTokenCommand : ICommand<TokenResposeDto>
    {
        public string RefreshToken { get; set; }
    }
}
