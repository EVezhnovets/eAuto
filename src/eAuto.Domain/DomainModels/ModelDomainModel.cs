using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using ModelDataM = eAuto.Data.Interfaces.DataModels.ModelDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class ModelDomainModel : IModel
    {
        private readonly IRepository<ModelDataM> _modelRepository;
        private string _name;
        private readonly bool _isNew;

        public int ModelId { get; set; }
        public string Name { get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ModelNotFoundException();
                }
                _name = value;
            } 
        }
        public int BrandId { get; set; }
        public string Brand { get; set; }

        public ModelDomainModel()
        {
        }

        public ModelDomainModel(IRepository<ModelDataM> modelRepository)
        {
            _modelRepository = modelRepository;
            _isNew = true;
        }

        internal ModelDomainModel(
            ModelDataM modelDataModel,
            IRepository<ModelDataM> modelRepository)
        {
            _modelRepository = modelRepository;

            ModelId = modelDataModel.ModelId;
            _name = modelDataModel.Name;
            BrandId = modelDataModel.BrandId;
            Brand = modelDataModel.Brand.Name.ToString();
        }

		public void Save()
		{
			var modelDataModel = GetModelDataModel();

            if (_isNew)
            {
                var result = _modelRepository.Create(modelDataModel);
                ModelId = result.ModelId;
                BrandId = result.BrandId;
            }
            else
            {
                _modelRepository.Update(modelDataModel);
            }
        }

		public void Delete()
		{
            var modelModel = GetModelDataModel();
            if (!_isNew)
            {
                _modelRepository.Delete(modelModel);
            }
		}

        private ModelDataM GetModelDataModel()
        {
            var modelDataM = new ModelDataM
            {
                ModelId = ModelId,
                Name = Name,
                BrandId = BrandId,
            };
            return modelDataM;
		}
	}
}