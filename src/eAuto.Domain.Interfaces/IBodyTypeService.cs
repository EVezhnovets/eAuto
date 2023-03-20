namespace eAuto.Domain.Interfaces
{
    public interface IBodyTypeService
    {
        IBodyType GetBodyTypeModel(int id);
        Task<IEnumerable<IBodyType>> GetBodyTypeModelsAsync();
        Task<IBodyType> CreateBodyTypeModelAsync(IBodyType bodyType);
		Task<IBodyType> CreateBodyTypeModelAsync(string name);
	}
}
