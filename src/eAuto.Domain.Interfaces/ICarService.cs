namespace eAuto.Domain.Interfaces
{
    public interface ICarService
    {
		ICar GetCarModel(int id);
        Task<IEnumerable<ICar>> GetCarModelsAsync();
		ICar CreateCarDomainModel();
    }
}