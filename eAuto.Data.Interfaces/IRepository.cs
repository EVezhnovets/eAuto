namespace eAuto.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync<Tid>(Tid id);
        Task<T> GetAsync<Tid>(T obj);
        Task<T> AddAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(T obj);
    }
}
