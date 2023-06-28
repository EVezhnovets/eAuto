using eAuto.Data;
using eAuto.Data.Context;
using eAuto.Data.Interfaces.DataModels;
using Microsoft.EntityFrameworkCore;
using PublicApi.Exceptions;

namespace PublicApi.Services
{
    public class CarsApiService
    {
        private readonly EAutoContext? _context;
        private readonly Repository<CarDataModel> _repository;
        private readonly ILogger<CarsApiService> _logger;

        public CarsApiService(
            EAutoContext context,
            Repository<CarDataModel> repository,
            ILogger<CarsApiService> logger)
        {
            _context = context;
            _repository = repository;
            _logger = logger;
        }

        public List<CarDataModel> GetAllCars() 
        {
            var carList = _repository
            .GetAllAsync(
            include: query => query
                .Include(e => e.Brand!)
                .Include(e => e.Model!)
                .Include(e => e.Generation!)
                .Include(e => e.BodyType!)
                .Include(e => e.DriveType!)
                .Include(e => e.Transmission!)
            ).Result.ToList();

            if(carList != null)
            {
                return carList;
            }
            else
            {
                _logger.LogError("CarsApiService.GetAllCars(), GenericNotFoundException. Cars not found");
                throw new GenericNotFoundException<CarsApiService>("Cars not found");
            }
        } 

        public CarDataModel GetCarById(int id)
        {
            var carResult = _repository.Get(i => i.CarId == id);

            if (carResult != null)
            {
                return carResult;
            }
            else
            {
                _logger.LogError("CarsApiService.GetAllCars(), GenericNotFoundException. Cars not found");
                throw new GenericNotFoundException<CarsApiService>("Car not found");
            }
        }
    }
}