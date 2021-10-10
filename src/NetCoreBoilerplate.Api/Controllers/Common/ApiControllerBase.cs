using Microsoft.AspNetCore.Mvc;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;

namespace NetCoreBoilerplate.Api.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;

        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
