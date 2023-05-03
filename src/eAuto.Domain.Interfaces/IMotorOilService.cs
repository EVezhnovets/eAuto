namespace eAuto.Domain.Interfaces
{
    public interface IMotorOilService
	{
		IMotorOil GetMotorOilModel(int id);
        Task<IEnumerable<IMotorOil>> GetMotorOilModelsAsync();
		IMotorOil CreateMotorOilDomainModel();
    }
}