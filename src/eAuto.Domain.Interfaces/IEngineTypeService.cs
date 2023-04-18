namespace eAuto.Domain.Interfaces
{
    public interface IEngineTypeService
	{
		IEngineType GetEngineType(int id);
        Task<IEnumerable<IEngineType>> GetEngineTypesAsync();
    }
}