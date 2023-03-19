namespace eAuto.Domain.Interfaces
{
    public interface IBodyTypeService
    {
        Task<IBodyType> GetBodyTypeModelAsync(int id);
        Task<IEnumerable<IBodyType>> GetBodyTypeModelsAsync();
        Task<IBodyType> CreateBodyTypeModelAsync(IBodyType bodyType);
		Task<IBodyType> CreateBodyTypeModelAsync(string name);
	}
}
