namespace eAuto.Domain.Interfaces
{
    public interface IGenerationService
    {
		IGeneration GetGenerationModel(int id);
        Task<IEnumerable<IGeneration>> GetGenerationModelsAsync();
        IGeneration CreateGenerationDomainModel();
    }
}
