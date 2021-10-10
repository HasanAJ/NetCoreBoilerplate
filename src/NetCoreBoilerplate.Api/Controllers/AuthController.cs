using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreBoilerplate.Api.Controllers.Common;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.User.Authenticate;
using NetCoreBoilerplate.Application.User.RefreshToken;

namespace NetCoreBoilerplate.Api.Controllers
{
    public class AuthController : ApiControllerBase
    {
        public AuthController(IMediator _mediator)
            : base(_mediator)
        {
        }

        [HttpPost("token")]
        public async Task<ActionResult<TokenResposeDto>> Authenticate(AuthenticateCommand command, CancellationToken ct)
        {
            return await _mediator.Send(command, ct);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<TokenResposeDto>> RefreshToken(RefreshTokenCommand command, CancellationToken ct)
        {
            return await _mediator.Send(command, ct);
        }
    }
}
