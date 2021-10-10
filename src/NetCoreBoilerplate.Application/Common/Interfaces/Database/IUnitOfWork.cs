using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Domain.Common;

namespace NetCoreBoilerplate.Application.Common.Interfaces.Database
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : Entity;

        T GetCustomRepository<T>() where T : class;

        Task<int> SaveChanges(CancellationToken ct = default(CancellationToken));

        Task Dispose();
    }
}
