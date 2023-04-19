using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Domain.Services
{
    public sealed class MotorOilService : IMotorOilService
	{
        private readonly IRepository<MotorOilDataModel> _motorOilRepository;
        private readonly IAppLogger<MotorOilService> _logger;

        public MotorOilService(
            IRepository<MotorOilDataModel> motorOilRepository,
            IAppLogger<MotorOilService> logger)
        {
            _motorOilRepository = motorOilRepository;
            _logger = logger;
        }

        public IMotorOil GetMotorOilModel(int id)
        {
            var motorOilDataModel = GetMotorOil(id);

            if (motorOilDataModel == null)
            {
				var exception = new MotorOilNotFoundException("MotorOil not found");
				_logger.LogError(exception, exception.Message);
			}

            var motorOilViewModel = new MotorOilDomainModel(motorOilDataModel, _motorOilRepository);
            return motorOilViewModel;
        }

        public async Task<IEnumerable<IMotorOil>> GetMotorOilModelsAsync()
        {
            var motorOilEntities = await _motorOilRepository
                .GetAllAsync(
                include: query => query
                .Include(g => g.ProductBrand)
                );

            if (motorOilEntities == null)
            {
				var exception = new MotorOilNotFoundException("MotorOil not found");
				_logger.LogError(exception, exception.Message);
			}

            var motorOilViewModels = motorOilEntities
                .Select(i => new MotorOilDomainModel()
                {
                    MotorOilId = i.MotorOilDataModelId,
                    Name = i.Name,
                    Viscosity = i.Viscosity,
                    Composition = i.Composition,
                    Volume = i.Volume,
                    ProductBrandDataModelId = i.ProductBrandDataModelId,
                    ProductBrand = i.ProductBrand.ToString(),
				}).ToList();
            var motorOilModels = motorOilViewModels.Cast<IMotorOil>();
            return motorOilModels;
        }
 
        public IMotorOil CreateMotorOilDomainModel()
        {
            var motorOil = new MotorOilDomainModel(_motorOilRepository);
            return motorOil;
        }

        public MotorOilDataModel GetMotorOil(int motorOilId)
        {
            var motorOil = _motorOilRepository
                .Get(
                    predicate: bt => bt.MotorOilDataModelId == motorOilId, include: query => query
                        .Include(g => g.ProductBrand)
                );

            if (motorOil == null)
            {
				var exception = new MotorOilNotFoundException("MotorOil not found");
				_logger.LogError(exception, exception.Message);
			}
            return motorOil;
        }
    }
}