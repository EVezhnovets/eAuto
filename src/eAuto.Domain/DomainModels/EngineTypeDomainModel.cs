using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using EngineTypeDataM = eAuto.Data.Interfaces.DataModels.EngineTypeDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class EngineTypeDomainModel : IEngineType
	{
        private readonly IRepository<EngineTypeDataM> _engineTypeRepository;
        private string _name;

        public int EngineTypeId { get; set; }
        public string Name { get; set; }

        public EngineTypeDomainModel() { }

        internal EngineTypeDomainModel(
            EngineTypeDataM engineTypeDataModel,
            IRepository<EngineTypeDataM> engineTypeRepository)
        {
			_engineTypeRepository = engineTypeRepository;
            _name = engineTypeDataModel.Name;

			EngineTypeId = engineTypeDataModel.EngineTypeId;
        }

        private EngineTypeDataM GetEngineTypeDataModel()
        {
            var engineTypeDataM = new EngineTypeDataM
			{
                EngineTypeId = EngineTypeId,
                Name = Name
			};
            return engineTypeDataM;
		}
	}
}