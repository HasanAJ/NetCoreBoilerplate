using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreBoilerplate.Application.Common.Interfaces.Database;
using NetCoreBoilerplate.Domain.Common;
using NetCoreBoilerplate.Infrastructure.Common.Extensions;
using NetCoreBoilerplate.Infrastructure.Database.Context;

namespace NetCoreBoilerplate.Infrastructure.Common.Database
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IPagedList<T>> GetPagedList(Expression<Func<T, bool>> predicate = null,
                                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                                    int page = 0,
                                                                    int pageSize = 20,
                                                                    bool disableTracking = true,
                                                                    CancellationToken ct = default(CancellationToken),
                                                                    bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _db.Set<T>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToPagedListAsync(page, pageSize);
            }
            else
            {
                return await query.ToPagedListAsync(page, pageSize);
            }
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null,
                                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                                    int page = 0,
                                                                    int pageSize = 20,
                                                                    bool disableTracking = true,
                                                                    CancellationToken ct = default(CancellationToken),
                                                                    bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _db.Set<T>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(ct);
            }
            else
            {
                return await query.ToListAsync(ct);
            }
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate = null,
                                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                                    int page = 0,
                                                                    int pageSize = 20,
                                                                    bool disableTracking = true,
                                                                    CancellationToken ct = default(CancellationToken),
                                                                    bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _db.Set<T>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync(ct);
            }
            else
            {
                return await query.FirstOrDefaultAsync(ct);
            }
        }

        public Task<T> Find(int id, CancellationToken ct = default(CancellationToken))
        {
            return _db.Set<T>().SingleOrDefaultAsync(m => m.Id == id, ct);
        }

        public async Task Insert(T entity, CancellationToken ct = default(CancellationToken))
        {
            await _db.Set<T>().AddAsync(entity, ct);
        }

        public async Task Insert(List<T> entities, CancellationToken ct = default(CancellationToken))
        {
            await _db.Set<T>().AddRangeAsync(entities, ct);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }

        public void Update(List<T> entities)
        {
            _db.Set<T>().UpdateRange(entities);
        }

        public async Task Remove(int id, CancellationToken ct = default(CancellationToken))
        {
            T entity = await _db.Set<T>().SingleOrDefaultAsync(m => m.Id == id, ct);

            _db.Set<T>().Remove(entity);
        }

        public void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void Remove(List<T> entities)
        {
            _db.Set<T>().RemoveRange(entities);
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate = null, CancellationToken ct = default(CancellationToken))
        {
            if (predicate == null)
            {
                return await _db.Set<T>().CountAsync(ct);
            }
            else
            {
                return await _db.Set<T>().CountAsync(predicate, ct);
            }
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> predicate = null, CancellationToken ct = default(CancellationToken))
        {
            if (predicate == null)
            {
                return await _db.Set<T>().AnyAsync(ct);
            }
            else
            {
                return await _db.Set<T>().AnyAsync(predicate, ct);
            }
        }
    }
}
