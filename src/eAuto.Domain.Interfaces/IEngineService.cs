namespace eAuto.Domain.Interfaces
{
    public interface IEngineService
    {
		IEngine GetEngineModel(int id);
        Task<IEnumerable<IEngine>> GetEngineModelsAsync();
		IEngine CreateEngineDomainModel();
    }
}