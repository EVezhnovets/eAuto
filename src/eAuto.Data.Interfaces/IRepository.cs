namespace eAuto.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {        
        Task<List<T>> GetAllAsync();
        T Create(T obj);
        void Update(T obj);
        void Delete(T obj);
        T? Get(Func<T, bool> func);
    }
}
