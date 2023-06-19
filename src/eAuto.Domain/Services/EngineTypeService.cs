using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;

namespace eAuto.Domain.Services
{
    public sealed class EngineTypeService : IEngineTypeService
	{
        private readonly IRepository<EngineTypeDataModel> _engineTypeRepository;
        private readonly IAppLogger<EngineTypeService> _logger;

        public EngineTypeService(
            IRepository<EngineTypeDataModel> engineTypeRepository,
            IAppLogger<EngineTypeService> logger)
        {
			_engineTypeRepository = engineTypeRepository;
            _logger = logger;
        }

        public IEngineType GetEngineType(int id)
        {
            var engineTypeDataModel = GetEngine(id);

            if (engineTypeDataModel == null)
            {
				var exception = new GenericNotFoundException<EngineTypeService>("EngineType not found");
				_logger.LogError(exception, exception.Message);
			}

            var engineTypeViewModel = 
                new EngineTypeDomainModel(engineTypeDataModel!, _engineTypeRepository);
            return engineTypeViewModel;
        }

        public async Task<IEnumerable<IEngineType>> GetEngineTypesAsync()
        {
            var engineTypeEntities = await _engineTypeRepository.GetAllAsync();

            if (engineTypeEntities == null)
            {
				var exception = new GenericNotFoundException<EngineTypeService>("EngineType not found");
				_logger.LogError(exception, exception.Message);
			}

            var engineTypeViewModels = engineTypeEntities!
                .Select(i => new EngineTypeDomainModel()
                {
                    EngineTypeId = i.EngineTypeId,
                    Name = i.Name,
                }).ToList();
            var engineTypeModels = engineTypeViewModels.Cast<IEngineType>();
            return engineTypeModels;
        }

        public EngineTypeDataModel GetEngine(int engineTypeId)
        {
            var engineType = _engineTypeRepository
				.Get(t => t.EngineTypeId == engineTypeId
				);

            if (engineType == null)
            {
				var exception = new GenericNotFoundException<EngineTypeService>("EngineType not found");
				_logger.LogError(exception, exception.Message);
			}

            return engineType!;
        }
    }
}