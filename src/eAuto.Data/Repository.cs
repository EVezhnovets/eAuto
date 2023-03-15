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

        public async Task<List<T>> GetAllAsync()
        {
            return await _eAutoContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync<Tid>(Tid id)
        {
            return await _eAutoContext.Set<T>().FindAsync(id);
        }
    }
}
