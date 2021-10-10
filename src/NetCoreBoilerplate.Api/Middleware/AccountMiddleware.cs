using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetCoreBoilerplate.Application.Common.Exceptions;
using NetCoreBoilerplate.Application.Common.Interfaces.Database;
using NetCoreBoilerplate.Domain.Entities;

namespace NetCoreBoilerplate.Api.Middleware
{
    public class AccountMiddleware
    {
        private readonly RequestDelegate _next;

        public AccountMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IUnitOfWork uow)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                string userId = httpContext.User.Claims.First(x => x.Type == "id").Value;

                var user = await uow.GetRepository<Account>().Find(id: (Convert.ToInt32(userId)));

                if (user == null)
                    throw new UnauthorizedException(nameof(Account), nameof(Account.Id));

                httpContext.Items["Account"] = user;
            }

            await _next(httpContext);
        }
    }
}
