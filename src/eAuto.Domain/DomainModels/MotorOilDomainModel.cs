using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using MotorOilDataM = eAuto.Data.Interfaces.DataModels.MotorOilDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class MotorOilDomainModel : IMotorOil
    {
        private readonly IRepository<MotorOilDataM> _motorOilRepository;
        private string _name;
        private readonly bool _isNew;

        public int MotorOilId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string Viscosity { get; set; }
		public string Composition { get; set; }
		public int Volume { get; set; }
		public int ProductBrandId { get; set; }
		public string ProductBrand { get; set; }

		public MotorOilDomainModel() { }

        internal MotorOilDomainModel(
            MotorOilDataM motorOilDataModel,
            IRepository<MotorOilDataM> motorOilRepository)
        {
            _motorOilRepository = motorOilRepository;

            MotorOilId = motorOilDataModel.MotorOilDataModelId;
            Name = motorOilDataModel.Name;
            PictureUrl = motorOilDataModel.PictureUrl;
            Price = motorOilDataModel.Price;
			Viscosity = motorOilDataModel.Viscosity;
            Composition = motorOilDataModel.Composition;
            Volume = motorOilDataModel.Volume;
            ProductBrandId = motorOilDataModel.ProductBrandId;
            ProductBrand = motorOilDataModel.ProductBrand.Name;
		}

        internal MotorOilDomainModel(
             IRepository<MotorOilDataM> motorOilRepository)
        {
            _motorOilRepository = motorOilRepository;
            _isNew = true;
        }

		public void Save()
		{
			var motorOilDataModel = GetMotorOilDataModel();

            if (_isNew)
            {
                var result = _motorOilRepository.Create(motorOilDataModel);
                MotorOilId = result.MotorOilDataModelId;
                ProductBrandId = result.ProductBrandId;
            }
            else
            {
                _motorOilRepository.Update(motorOilDataModel);
            }
        }

		public void Delete()
		{
            var motorOilModel = GetMotorOilDataModel();
            if (!_isNew)
            {
                _motorOilRepository.Delete(motorOilModel);
            }
		}

        private MotorOilDataM GetMotorOilDataModel()
        {
            var MotorOilDataM = new MotorOilDataM
            {
				MotorOilDataModelId = MotorOilId,
                Name = Name,
                PictureUrl = PictureUrl,
                Price = Price,
                Viscosity = Viscosity,
                Composition = Composition,
                Volume = Volume,
                ProductBrandId = ProductBrandId
            };
            return MotorOilDataM;
		}
	}
}