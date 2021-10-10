using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Domain.Common;

namespace NetCoreBoilerplate.Application.Common.Interfaces.Database
{
    public interface IRepository<T> where T : Entity
    {
        Task<IPagedList<T>> GetPagedList(Expression<Func<T, bool>> predicate = null,
                                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                       int page = 0,
                                                       int pageSize = 20,
                                                       bool disableTracking = true,
                                                       CancellationToken ct = default(CancellationToken),
                                                       bool ignoreQueryFilters = false);

        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null,
                                                                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                                   int page = 0,
                                                                   int pageSize = 20,
                                                                   bool disableTracking = true,
                                                                   CancellationToken ct = default(CancellationToken),
                                                                   bool ignoreQueryFilters = false);

        Task<T> Find(Expression<Func<T, bool>> predicate = null,
                                                               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                               int page = 0,
                                                               int pageSize = 20,
                                                               bool disableTracking = true,
                                                               CancellationToken ct = default(CancellationToken),
                                                               bool ignoreQueryFilters = false);

        Task<T> Find(int id, CancellationToken ct = default(CancellationToken));

        Task Insert(T entity, CancellationToken ct = default(CancellationToken));

        Task Insert(List<T> entities, CancellationToken ct = default(CancellationToken));

        void Update(T entity);

        void Update(List<T> entities);

        Task Remove(int id, CancellationToken ct = default(CancellationToken));

        void Remove(T entity);

        void Remove(List<T> entities);

        Task<int> Count(Expression<Func<T, bool>> predicate = null, CancellationToken ct = default(CancellationToken));

        Task<bool> Exists(Expression<Func<T, bool>> selector = null, CancellationToken ct = default(CancellationToken));

    }
}
