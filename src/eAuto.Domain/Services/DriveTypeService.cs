using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;

namespace eAuto.Domain.Services
{
    public sealed class DriveTypeService : IDriveTypeService
	{
        private readonly IRepository<DriveTypeDataModel> _driveTypeRepository;

        public DriveTypeService(IRepository<DriveTypeDataModel> driveTypeRepository)
        {
            _driveTypeRepository = driveTypeRepository;
        }

        public IDriveType GetDriveTypeModel(int id)
        {
            var driveTypeDataModel = GetDriveType(id);

            if (driveTypeDataModel == null)
            {
                throw new DriveTypeNotFoundException();
            }

            var driveTypeViewModel = new DriveTypeDomainModel(driveTypeDataModel, _driveTypeRepository);
            return driveTypeViewModel;
        }

        public async Task<IEnumerable<IDriveType>> GetDriveTypeModelsAsync()
        {
            var driveTypeEntities = await _driveTypeRepository.GetAllAsync();

            if (driveTypeEntities == null)
            {
                throw new DriveTypeNotFoundException();
            }

            var driveTypeViewModels = driveTypeEntities
                .Select(i => new DriveTypeDomainModel()
                {
                    DriveTypeId = i.DriveTypeId,
                    Name = i.Name,
                }).ToList();
            var driveTypeModels = driveTypeViewModels.Cast<IDriveType>();
            return driveTypeModels;
        }
 
        public IDriveType CreateDriveTypeDomainModel()
        {
            var driveType = new DriveTypeDomainModel(_driveTypeRepository);
            return driveType;
        }

        public DriveTypeDataModel GetDriveType(int driveTypeId)
        {
            var driveType = _driveTypeRepository
                .Get(t => t.DriveTypeId == driveTypeId
                );

            if (driveType == null)
            {
                throw new DriveTypeNotFoundException();
            }
            return driveType;
        }
    }
}