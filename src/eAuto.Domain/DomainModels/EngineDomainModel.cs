using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using EngineDataM = eAuto.Data.Interfaces.DataModels.EngineDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class EngineDomainModel : IEngine
    {
        private readonly IRepository<EngineDataM> _engineRepository;

        private string _identificationName;
        private readonly bool _isNew;
        
        ///TODO null check
        public int EngineId { get; set; }
        public string IdentificationName { get => _identificationName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EngineNameNotFoundException();
                }
				_identificationName = value;
            } 
        }
		public string Type { get; set; }
		public int Capacity { get; set; }
		public int Power { get; set; }
		public string Description { get; set; }
		public int GenerationId { get; set; }
		public string Generation { get; set; }
		public int BrandId { get; set; }
		public string Brand { get; set; }
		public int ModelId { get; set; }
		public string Model { get; set; }

		public EngineDomainModel()
        {
        }

        public EngineDomainModel(IRepository<EngineDataM> engineRepository)
        {
            _engineRepository = engineRepository;
            _isNew = true;
        }

        internal EngineDomainModel(
            EngineDataM engineDataModel,
            IRepository<EngineDataM> engineRepository)
        {
            _engineRepository = engineRepository;
            _identificationName = engineDataModel.IdentificationName;
            Type = engineDataModel.Type;
            Capacity = engineDataModel.Capacity;
            Power = engineDataModel.Power;
            Description = engineDataModel.Description;
            GenerationId = engineDataModel.GenerationId;
            Generation = engineDataModel.Generation.ToString();
            EngineId = engineDataModel.EngineId;
            BrandId = engineDataModel.BrandId;
            Brand = engineDataModel.Brand.Name.ToString();
            ModelId = engineDataModel.ModelId;
            Model = engineDataModel.Model.Name.ToString();
        }

		public void Save()
		{
			var engineDataModel = GetEngineDataModel();

            if (_isNew)
            {
                var result = _engineRepository.Create(engineDataModel);
                EngineId = result.EngineId;
                ModelId = result.ModelId;
                BrandId = result.BrandId;
            }
            else
            {
                _engineRepository.Update(engineDataModel);
            }
        }

		public void Delete()
		{
            var engineModel = GetEngineDataModel();
            if (!_isNew)
            {
                _engineRepository.Delete(engineModel);
            }
		}

        private EngineDataM GetEngineDataModel()
        {
            var engineDataM = new EngineDataM
            {
                EngineId = EngineId,
                IdentificationName = IdentificationName,
                Type = Type,
                Capacity = Capacity,
                Power = Power,
                Description = Description,
                GenerationId = GenerationId,
                BrandId = BrandId,
				ModelId = ModelId,
			};
            return engineDataM;
		}
	}
}