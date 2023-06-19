using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using DriveTypeDataM = eAuto.Data.Interfaces.DataModels.DriveTypeDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class DriveTypeDomainModel : IDriveType
    {
        private readonly IRepository<DriveTypeDataM>? _driveTypeRepository;
        private readonly string? _name;
        private readonly bool _isNew;

        public int DriveTypeId { get; set; }
        public string? Name { get;set; }

        public DriveTypeDomainModel()
        {
        }

        public DriveTypeDomainModel(IRepository<DriveTypeDataM> driveTypeRepository)
        {
            _driveTypeRepository = driveTypeRepository;
            _isNew = true;
        }

        internal DriveTypeDomainModel(
            DriveTypeDataM driveTypeDataModel,
            IRepository<DriveTypeDataM> driveTypeRepository)
        {
            _driveTypeRepository = driveTypeRepository;
            _name = driveTypeDataModel.Name;

            DriveTypeId = driveTypeDataModel.DriveTypeId;
        }

		public void Save()
		{
			var driveTypeDataModel = GetDriveTypeDataModel();

            if (_isNew)
            {
                var result = _driveTypeRepository!.Create(driveTypeDataModel);
                DriveTypeId = result.DriveTypeId;
            }
            else
            {
                _driveTypeRepository!.Update(driveTypeDataModel);
            }
        }

		public void Delete()
		{
            var driveTypeModel = GetDriveTypeDataModel();
            if (!_isNew)
            {
                _driveTypeRepository!.Delete(driveTypeModel);
            }
		}

        private DriveTypeDataM GetDriveTypeDataModel()
        {
            var driveTypeDataM = new DriveTypeDataM
            {
                DriveTypeId = DriveTypeId,
                Name = Name
			};
            return driveTypeDataM;
		}
	}
}