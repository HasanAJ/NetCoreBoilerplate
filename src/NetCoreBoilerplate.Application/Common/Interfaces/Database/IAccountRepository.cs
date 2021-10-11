using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Domain.Entities;

namespace NetCoreBoilerplate.Application.Common.Interfaces.Database
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByEmail(string email, CancellationToken ct = default(CancellationToken));

        Task<Account> GetByRefreshToken(string refreshToken, CancellationToken ct = default(CancellationToken));
    }
}
