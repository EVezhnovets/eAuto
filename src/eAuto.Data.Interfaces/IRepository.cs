using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace eAuto.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {        
        Task<List<T>> GetAllAsync();
        T Create(T obj);
        void Update(T obj);
        void Delete(T obj);
        T? Get(Func<T, bool> func);


        //TODO Remove EnttityFramework reference
        public T? Get(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<IList<T>> GetAllAsync(
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    }
}