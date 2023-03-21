namespace eAuto.Domain.Interfaces
{
    public interface ITransmissionService
    {
		ITransmission GetTransmissionModel(int id);
        Task<IEnumerable<ITransmission>> GetTransmissionModelsAsync();
        ITransmission CreateTransmissionDomainModel();
    }
}