using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;

namespace eAuto.Domain.Services
{
    public sealed class BodyTypeService : IBodyTypeService
    {
        private readonly IRepository<BodyTypeDataModel> _bodyTypeRepository;
        private readonly IAppLogger<BodyTypeService> _logger;

        public BodyTypeService(
            IRepository<BodyTypeDataModel> bodyTypeRepository,
            IAppLogger<BodyTypeService> logger)
        {
            _bodyTypeRepository = bodyTypeRepository;
            _logger = logger;
        }

        public IBodyType GetBodyTypeModel(int id)
        {
            var bodyTypeDataModel = GetBodyType(id);

            if (bodyTypeDataModel == null)
            {
                var exception = new BodyTypeNotFoundException("BodyType not found");
                _logger.LogError(exception, exception.Message);
            }

            var bodyTypeViewModel = new BodyTypeDomainModel(bodyTypeDataModel, _bodyTypeRepository);
            return bodyTypeViewModel;
        }

        public async Task<IEnumerable<IBodyType>> GetBodyTypeModelsAsync()
        {
            var bodyTypeEntities = await _bodyTypeRepository.GetAllAsync();

            if(bodyTypeEntities == null)
            {
				var exception = new BodyTypeNotFoundException("BodyType not found");
				_logger.LogError(exception, exception.Message);
			}

            var bodyTypeViewModels = bodyTypeEntities
                .Select(i => new BodyTypeDomainModel()
                {
                    BodyTypeId = i.BodyTypeId,
                    Name = i.Name,
                }).ToList();
            var BodyTypeModels = bodyTypeViewModels.Cast<IBodyType>();
            return BodyTypeModels;
        }

        public IBodyType CreateBodyTypeModel(string name)
        {
            var bodyType = new BodyTypeDomainModel(_bodyTypeRepository, name);
            return bodyType;
        }

        public BodyTypeDataModel GetBodyType(int bodyTypeId)
        {
            var bodyType = _bodyTypeRepository
                .Get(bt => bt.BodyTypeId == bodyTypeId);

            if(bodyType == null)
            {
				var exception = new BodyTypeNotFoundException("BodyType not found");
				_logger.LogError(exception, exception.Message);
			}

            return bodyType;
        }
	}
}