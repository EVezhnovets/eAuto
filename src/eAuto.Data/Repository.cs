using eAuto.Data.Context;
using eAuto.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Storage
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EAutoContext _eAutoContext;
        public Repository(EAutoContext eAutoContext)
        {
            _eAutoContext = eAutoContext;
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
            _eAutoContext.Set<T>().Update(obj);
            _eAutoContext.SaveChanges();
        }

        public void Delete(T obj)
        {
            _eAutoContext.Set<T>().Remove(obj);
            _eAutoContext.SaveChanges();
        }
    }
}