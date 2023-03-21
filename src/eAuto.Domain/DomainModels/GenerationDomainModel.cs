using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using GenerationDataM = eAuto.Data.Interfaces.DataModels.GenerationDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class GenerationDomainModel : IGeneration
    {
        private readonly IRepository<GenerationDataM> _generationRepository;
        private string _name;
        private readonly bool _isNew;

        public int GenerationId { get; set; }
        public string Name { get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new GenerationNotFoundException();
                }
                _name = value;
            } 
        }
        public int BrandId { get; set; }
        public string Brand { get; set; }
        public int ModelId { get; set; }
        public string Model { get; set; }

        public GenerationDomainModel()
        {
        }

        public GenerationDomainModel(IRepository<GenerationDataM> generationRepository)
        {
            _generationRepository = generationRepository;
            _isNew = true;
        }

        internal GenerationDomainModel(
            GenerationDataM generationDataModel,
            IRepository<GenerationDataM> generationRepository)
        {
            _generationRepository = generationRepository;
            _name = generationDataModel.Name;

            GenerationId = generationDataModel.GenerationId;
            BrandId = generationDataModel.BrandId;
            Brand = generationDataModel.Brand.Name.ToString();
            ModelId = generationDataModel.ModelId;
            Model = generationDataModel.Model.Name.ToString();
        }

        internal GenerationDomainModel(
             IRepository<GenerationDataM> generationRepository, string name)
        {
			_generationRepository = generationRepository;
            _isNew = true;

            Name = name;
        }

		public void Save()
		{
			var generationDataModel = GetGenerationDataModel();

            if (_isNew)
            {
                var result = _generationRepository.Create(generationDataModel);
                GenerationId = result.GenerationId;
                ModelId = result.ModelId;
                BrandId = result.BrandId;
            }
            else
            {
                _generationRepository.Update(generationDataModel);
            }
        }

		public void Delete()
		{
            var generationModel = GetGenerationDataModel();
            if (!_isNew)
            {
                _generationRepository.Delete(generationModel);
            }
		}

        private GenerationDataM GetGenerationDataModel()
        {
            var generationDataM = new GenerationDataM
            {
                GenerationId = GenerationId,
                Name = Name,
                BrandId = BrandId,
				ModelId = ModelId,
			};
            return generationDataM;
		}
	}
}