namespace eAuto.Domain.Interfaces
{
    public interface IModelService
    {
        IModel GetModelModel(int id);
        Task<IEnumerable<IModel>> GetModelModelsAsync();
        IModel CreateModelDomainModel();
    }
}
