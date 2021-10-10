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
    }
}
