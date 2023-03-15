namespace eAuto.Domain.Interfaces
{
    public interface IBodyTypeService
    {
        Task<IBodyType> GetBodyTypeViewModelAsync(int id);
        Task<IEnumerable<IBodyType>> GetBodyTypeViewModelsAsync();
    }
}
