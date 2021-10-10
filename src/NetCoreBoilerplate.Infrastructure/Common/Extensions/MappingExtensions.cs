using System.Linq;
using System.Threading.Tasks;
using NetCoreBoilerplate.Infrastructure.Common.Models;

namespace NetCoreBoilerplate.Infrastructure.Common.Extensions
{
    public static class MappingExtensions
    {
        public static Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
            => PagedList<T>.Create(queryable, pageNumber, pageSize);
    }
}
