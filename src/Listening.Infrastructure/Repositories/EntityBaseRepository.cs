using Listening.Core.Entities.Custom;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
            where T : class, IEntityBase, new()
    {

        protected readonly ApplicationDbContext _context;

        #region Properties
        public EntityBaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<PagedData<T>> GetPagedDataAsync(QueryViewModel query)
        {
            var dbSet = _context.Set<T>();
            var countTask = dbSet.CountAsync();

            var ordered = dbSet.OrderByDescending(x => x.Id);
            var resultTask = dbSet.Skip((query.Page - 1) * query.ElementsPerPage)
                .Take(query.ElementsPerPage).ToArrayAsync();

            await Task.WhenAll(resultTask, countTask);

            var pagedData = new PagedData<T>
            {
                Count = countTask.Result,
                Data = resultTask.Result
            };

            return pagedData;
        }

        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public virtual async Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public T GetSingle(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public async Task<T> GetSingleAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            _context.Set<T>().Add(entity);
        }

        public virtual async Task<long> AddNowAsync(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            var result = _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public virtual async Task AddManyAsync(T[] entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public virtual async Task AddManyNowAsync(T[] entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            _context.SaveChanges();
        }

        public virtual void Edit(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }

        public virtual async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
