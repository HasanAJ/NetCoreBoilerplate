using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Domain.Entities;

namespace NetCoreBoilerplate.Api.Services
{
    public class ContextService : IContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        // TODO: use constant keys
        public Account Account => (Account)_httpContextAccessor.HttpContext?.Items["Account"];
    }
}
