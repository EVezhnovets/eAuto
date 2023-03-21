namespace eAuto.Domain.Interfaces
{
    public interface IDriveTypeService
    {
		IDriveType GetDriveTypeModel(int id);
        Task<IEnumerable<IDriveType>> GetDriveTypeModelsAsync();
        IDriveType CreateDriveTypeDomainModel();
    }
}