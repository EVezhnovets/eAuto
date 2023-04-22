using eAuto.Data.Context;
using eAuto.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace eAuto.Storage
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EAutoContext _eAutoContext;
        private readonly DbSet<T> _dbSet;

        public Repository(EAutoContext eAutoContext)
        {
            _eAutoContext = eAutoContext;
            _dbSet = _eAutoContext.Set<T>();
        }

        public T Create(T obj)
        {
            var result = _eAutoContext.Add(obj).Entity;
            _eAutoContext.SaveChanges();
            return result;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _eAutoContext.Set<T>().ToListAsync();
        }

		public void Update(T obj)
		{
            _eAutoContext.Set<T>().AsNoTracking();
			_eAutoContext.Set<T>().Update(obj);
            _eAutoContext.SaveChanges();
        }

        public void Delete(T obj)
        {
            _eAutoContext.Set<T>().Remove(obj);
            _eAutoContext.SaveChanges();
        }

        public T? Get(Func<T, bool> func)
        {
            var result = _eAutoContext.Set<T>()
                .AsNoTracking()
                .FirstOrDefault(func);
            return result;
        }

        public T? Get(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            query = query.AsNoTracking();
            query = query.Where(predicate);
            query = include(query);

            return query.FirstOrDefault();
        }

        public async Task<IList<T>> GetAllAsync(
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            query = query.AsNoTracking();
            query = include(query);

            return await query.ToListAsync();
        }
    }
}