using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Domain.Services
{
    public sealed class CarService : ICarService
	{
        private readonly IRepository<CarDataModel> _carRepository;
        private readonly IAppLogger<CarService> _logger;

        public CarService(
            IRepository<CarDataModel> carRepository,
            IAppLogger<CarService> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }

        public ICar GetCarModel(int id)
        {
            var carDataModel = GetCar(id);

            if (carDataModel == null)
            {
				var exception = new GenericNotFoundException<CarService>("Car not found");
				_logger.LogError(exception, exception.Message);
			}

            var carViewModel = new CarDomainModel(carDataModel, _carRepository);
            return carViewModel;
        }

        public async Task<IEnumerable<ICar>> GetCarModelsAsync()
        {
            var carEntities = await _carRepository
                .GetAllAsync(
                include: query => query
                .Include(e => e.Brand)
                .Include(e => e.Model)
                .Include(e => e.Generation)
                .Include(e => e.BodyType)
                .Include(e => e.DriveType)
                .Include(e => e.Transmission)
                );

            if (carEntities == null)
            {
				var exception = new GenericNotFoundException<CarService>("Car not found");
				_logger.LogError(exception, exception.Message);
			}

            var carViewModels = carEntities
                .Select(i => new CarDomainModel()
                {
                    CarId = i.CarId,
                    PriceInitial = i.PriceInitial,
                    PictureUrl = i.PictureUrl,
                    Year = i.Year,
                    DateArrival = i.DateArrival,
                    Odometer = i.Odometer,
                    Description = i.Description,
                    EngineIdentificationName = i.EngineIdentificationName,
                    EngineCapacity = i.EngineCapacity,
                    EngineFuelType = i.EngineFuelType,
                    EngineFuelTypeId = i.EngineFuelTypeId,
                    EnginePower = i.EnginePower,
                    BrandId = i.BrandId,
                    Brand = i.Brand.Name,
                    ModelId = i.ModelId,
                    Model = i.Model.Name,
                    GenerationId = i.GenerationId,
                    Generation = i.Generation.Name,
                    BodyTypeId = i.BodyTypeId,
                    BodyType = i.BodyType.Name,
                    DriveTypeId = i.DriveTypeId,
                    DriveType = i.DriveType.Name,
                    TransmissionId = i.TransmissionId,
                    Transmission = i.Transmission.Name
                }).ToList();
            var carModels = carViewModels.Cast<ICar>();
            return carModels;
        }
 
        public ICar CreateCarDomainModel()
        {
            var car = new CarDomainModel(_carRepository);
            return car;
        }

        public CarDataModel GetCar(int carId)
        {
            var car = _carRepository
                .Get(
                    predicate: bt => bt.CarId == carId, include: query => query
                        .Include(g => g.Brand)
                        .Include(g => g.Model)
                        .Include(g => g.Generation)
                        .Include(g => g.BodyType)
                        .Include(g => g.DriveType)
                        .Include(g => g.Transmission)
                );

            if (car == null)
            {
				var exception = new GenericNotFoundException<CarService>("Car not found");
				_logger.LogError(exception, exception.Message);
			}
            return car;
        }
    }
}