using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreBoilerplate.Api.Controllers.Common;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.User.ChangePassword;
using NetCoreBoilerplate.Application.User.RegisterUser;
using NetCoreBoilerplate.Application.User.VerifyAccount;

namespace NetCoreBoilerplate.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        public UsersController(IMediator _mediator)
            : base(_mediator)
        {
        }

        [HttpPost("")]
        public async Task<ActionResult> RegisterUser(RegisterUserCommand command, CancellationToken ct)
        {
            await _mediator.Send(command, ct);
            return NoContent();
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordCommand command, CancellationToken ct)
        {
            await _mediator.Send(command, ct);
            return NoContent();
        }

        [HttpPost("verification")]
        public async Task<ActionResult> VerifyUser(VerifyAccountCommand command, CancellationToken ct)
        {
            await _mediator.Send(command, ct);
            return NoContent();
        }
    }
}
