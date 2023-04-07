using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using CarDataM = eAuto.Data.Interfaces.DataModels.CarDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class CarDomainModel : ICar
    {
        private readonly IRepository<CarDataM> _carRepository;
        private readonly bool _isNew;
        
        public int CarId { get; set; }
        public decimal PriceInitial { get; set; }
        public string PictureUrl { get; set; }
        public DateTime Year { get; set; }
        public DateTime DateArrival { get; set; }
        public int Odometer { get; set; }
        public string Description { get; set; }
		public string? EngineIdentificationName { get; set; }
		public string EngineFuelType { get; set; }
		public int EngineCapacity { get; set; }
		public int EnginePower { get; set; }

		public int BrandId { get; set; }
		public string Brand { get; set; }

		public int ModelId { get; set; }
		public string Model { get; set; }

        public int GenerationId { get; set; }
        public string Generation { get; set; }

        public int BodyTypeId { get; set; }
        public string BodyType { get; set; }

        public int DriveTypeId { get; set; }
        public string DriveType { get; set; }

        public int TransmissionId { get; set; }
        public string Transmission { get; set; }

        public CarDomainModel()
        {
        }

        public CarDomainModel(IRepository<CarDataM> carRepository)
        {
            _carRepository = carRepository;
            _isNew = true;
        }

        internal CarDomainModel(
            CarDataM carDataModel,
            IRepository<CarDataM> carRepository)
        {
            _carRepository = carRepository;
            CarId = carDataModel.CarId;
            PriceInitial = carDataModel.PriceInitial;
            PictureUrl = carDataModel.PictureUrl;
            Year = carDataModel.Year;
            DateArrival = carDataModel.DateArrival;
            Odometer = carDataModel.Odometer;
            Description = carDataModel.Description;
            EngineIdentificationName = carDataModel.EngineIdentificationName;
            EngineFuelType = carDataModel.EngineFuelType;
            EngineCapacity = carDataModel.EngineCapacity;
            EnginePower = carDataModel.EnginePower;
			BrandId = carDataModel.BrandId;
            Brand = carDataModel.Brand.Name;
            ModelId = carDataModel.ModelId;
            Model = carDataModel.Model.Name;
            GenerationId = carDataModel.GenerationId;
            Generation = carDataModel.Generation.Name;
            BodyTypeId = carDataModel.BodyTypeId;
            BodyType = carDataModel.BodyType.Name;
            DriveTypeId = carDataModel.DriveTypeId;
            DriveType = carDataModel.DriveType.Name;
            TransmissionId = carDataModel.TransmissionId;
            Transmission = carDataModel.Transmission.Name;
        }

		public void Save()
		{
			var carDataModel = GetCarDataModel();

            if (_isNew)
            {
                var result = _carRepository.Create(carDataModel);
                CarId = result.CarId;
                BrandId = result.BrandId;
                ModelId = result.ModelId;
                GenerationId = result.GenerationId;
                BodyTypeId = result.BodyTypeId;
                DriveTypeId = result.DriveTypeId;
                TransmissionId = result.TransmissionId;
            }
            else
            {
                _carRepository.Update(carDataModel);
            }
        }

		public void Delete()
		{
            var carModel = GetCarDataModel();
            if (!_isNew)
            {
                _carRepository.Delete(carModel);
            }
		}

        private CarDataM GetCarDataModel()
        {
            var carDataM = new CarDataM
            {
                CarId = CarId,
                PriceInitial = PriceInitial,
                PictureUrl = PictureUrl,
                Year = Year,
                DateArrival = DateArrival,
                Odometer = Odometer,
                Description = Description,
                EngineIdentificationName = EngineIdentificationName,
                EngineCapacity = EngineCapacity,
                EngineFuelType = EngineFuelType,
                EnginePower = EnginePower,
                BrandId = BrandId,
				ModelId = ModelId,
                GenerationId = GenerationId,
                BodyTypeId = BodyTypeId,
                DriveTypeId = DriveTypeId,
                TransmissionId = TransmissionId
            };
            return carDataM;
		}
	}
}