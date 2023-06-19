using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using BodyTypeDataM = eAuto.Data.Interfaces.DataModels.BodyTypeDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class BodyTypeDomainModel : IBodyType
    {
        private readonly IRepository<BodyTypeDataM>? _bodyTypeRepository;
        private readonly bool _isNew;

        public int BodyTypeId { get; set; }
        public string? Name { get; set; }
        public BodyTypeDomainModel()
        {

        }

        internal BodyTypeDomainModel(
            BodyTypeDataM bodyTypeDataModel,
            IRepository<BodyTypeDataM> bodyTypeRepository)
        {
            _bodyTypeRepository = bodyTypeRepository;

            BodyTypeId = bodyTypeDataModel.BodyTypeId;
            Name = bodyTypeDataModel.Name;

        }

        internal BodyTypeDomainModel(
             IRepository<BodyTypeDataM> bodyTypeRepository,string name)
        {
            _bodyTypeRepository = bodyTypeRepository;
            _isNew = true;

            Name = name;
        }

		public void Save()
		{
			var bodyTypeDataModel = GetBodyTypeDataModel();

            if (_isNew)
            {
                var result = _bodyTypeRepository!.Create(bodyTypeDataModel);
                BodyTypeId = result.BodyTypeId;
            }
            else
            {
                _bodyTypeRepository!.Update(bodyTypeDataModel);
            }
        }

		public void Delete()
		{
            var bodyTypeModel = GetBodyTypeDataModel();
            if (!_isNew)
            {
                _bodyTypeRepository!.Delete(bodyTypeModel);
            }
		}

        private BodyTypeDataM GetBodyTypeDataModel()
        {
            var bodyTypeDataM = new BodyTypeDataM
            {
                BodyTypeId = BodyTypeId,
                Name = Name
            };
            return bodyTypeDataM;
		}
	}
}