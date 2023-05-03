namespace eAuto.Domain.Interfaces
{
    public interface IBodyTypeService
    {
        IBodyType GetBodyTypeModel(int id);
        Task<IEnumerable<IBodyType>> GetBodyTypeModelsAsync();
		IBodyType CreateBodyTypeModel(string name);
	}
}
