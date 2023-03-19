namespace eAuto.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {        
        Task<List<T>> GetAllAsync();
        T Create(T obj);
        Task UpdateAsync(T obj);
        void Delete(T obj);
    }
}
