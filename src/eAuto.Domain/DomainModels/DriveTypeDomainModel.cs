using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using DriveTypeDataM = eAuto.Data.Interfaces.DataModels.DriveTypeDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class DriveTypeDomainModel : IDriveType
    {
        private readonly IRepository<DriveTypeDataM> _driveTypeRepository;
        private string _name;
        private readonly bool _isNew;

        public int DriveTypeId { get; set; }
        public string Name { get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new DriveTypeNotFoundException();
                }
                _name = value;
            } 
        }

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

        internal DriveTypeDomainModel(
             IRepository<DriveTypeDataM> driveTypeRepository, string name)
        {
			_driveTypeRepository = driveTypeRepository;
            _isNew = true;

            Name = name;
        }

		public void Save()
		{
			var driveTypeDataModel = GetDriveTypeDataModel();

            if (_isNew)
            {
                var result = _driveTypeRepository.Create(driveTypeDataModel);
                DriveTypeId = result.DriveTypeId;
            }
            else
            {
                _driveTypeRepository.Update(driveTypeDataModel);
            }
        }

		public void Delete()
		{
            var driveTypeModel = GetDriveTypeDataModel();
            if (!_isNew)
            {
                _driveTypeRepository.Delete(driveTypeModel);
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