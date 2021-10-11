using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreBoilerplate.Application.Common.Interfaces.Database;
using NetCoreBoilerplate.Domain.Entities;
using NetCoreBoilerplate.Infrastructure.Common.Database;
using NetCoreBoilerplate.Infrastructure.Database.Context;

namespace NetCoreBoilerplate.Infrastructure.Database.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext _db)
            : base(_db)
        {
        }

        public async Task<Account> GetByEmail(string email, CancellationToken ct = default(CancellationToken))
        {
            var account = await _db.Set<Account>()
                                    .Include(i => i.RefreshTokens)
                                    .SingleOrDefaultAsync(i => i.Email == email, ct);

            return account;
        }

        public async Task<Account> GetByRefreshToken(string refreshToken, CancellationToken ct = default(CancellationToken))
        {
            var account = await _db.Set<Account>()
                                    .Include(i => i.RefreshTokens)
                                    .SingleOrDefaultAsync(i => i.RefreshTokens.Any(t => t.Token == refreshToken), ct);

            return account;
        }
    }
}
