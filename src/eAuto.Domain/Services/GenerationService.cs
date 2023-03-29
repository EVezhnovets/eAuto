using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Domain.Services
{
    public sealed class GenerationService : IGenerationService
	{
        private readonly IRepository<GenerationDataModel> _generationRepository;
        private readonly IAppLogger<GenerationService> _logger;

        public GenerationService(
            IRepository<GenerationDataModel> generationRepository,
            IAppLogger<GenerationService> logger)
        {
            _generationRepository = generationRepository;
            _logger = logger;
        }

        public IGeneration GetGenerationModel(int id)
        {
            var generationDataModel = GetGeneration(id);

            if (generationDataModel == null)
            {
				var exception = new GenerationNotFoundException("Generation not found");
				_logger.LogError(exception, exception.Message);
			}

            var generationViewModel = new GenerationDomainModel(generationDataModel, _generationRepository);
            return generationViewModel;
        }

        public async Task<IEnumerable<IGeneration>> GetGenerationModelsAsync()
        {
            var generationEntities = await _generationRepository
                .GetAllAsync(
                include: query => query
                .Include(g => g.Brand)
                .Include(g => g.Model)
                );

            if (generationEntities == null)
            {
				var exception = new GenerationNotFoundException("Generation not found");
				_logger.LogError(exception, exception.Message);
			}

            var generationViewModels = generationEntities
                .Select(i => new GenerationDomainModel()
                {
                    GenerationId = i.GenerationId,
                    Name = i.Name,
                    BrandId = i.BrandId,
                    Brand = i.Brand.Name.ToString(),
					ModelId = i.ModelId,
                    Model = i.Model.Name.ToString()
				}).ToList();
            var generationModels = generationViewModels.Cast<IGeneration>();
            return generationModels;
        }
 
        public IGeneration CreateGenerationDomainModel()
        {
            var generation = new GenerationDomainModel(_generationRepository);
            return generation;
        }

        public GenerationDataModel GetGeneration(int generationId)
        {
            var generation = _generationRepository
                .Get(
                    predicate: bt => bt.GenerationId == generationId, include: query => query
                        .Include(g => g.Brand)
                        .Include(g => g.Model)
                );

            if (generation == null)
            {
				var exception = new GenerationNotFoundException("Generation not found");
				_logger.LogError(exception, exception.Message);
			}
            return generation;
        }
    }
}