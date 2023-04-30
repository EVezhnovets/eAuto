using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;

namespace eAuto.Domain.Services
{
    public sealed class TransmissionService : ITransmissionService
	{
        private readonly IRepository<TransmissionDataModel> _transmissionRepository;
        private readonly IAppLogger<TransmissionService> _logger;

        public TransmissionService(
            IRepository<TransmissionDataModel> transmissionRepository,
            IAppLogger<TransmissionService> logger)
        {
            _transmissionRepository = transmissionRepository;
            _logger = logger;
        }

        public ITransmission GetTransmissionModel(int id)
        {
            var transmissionDataModel = GetTransmission(id);

            if (transmissionDataModel == null)
            {
				var exception = new GenericNotFoundException<TransmissionService>("Transmission not found");
				_logger.LogError(exception, exception.Message);
			}

            var transmissionViewModel = 
                new TransmissionDomainModel(transmissionDataModel, _transmissionRepository);
            return transmissionViewModel;
        }

        public async Task<IEnumerable<ITransmission>> GetTransmissionModelsAsync()
        {
            var transmissionEntities = await _transmissionRepository.GetAllAsync();

            if (transmissionEntities == null)
            {
				var exception = new GenericNotFoundException<TransmissionService>("Transmission not found");
				_logger.LogError(exception, exception.Message);
			}

            var transmissionViewModels = transmissionEntities
                .Select(i => new TransmissionDomainModel()
                {
                    TransmissionId = i.TransmissionId,
                    Name = i.Name,
                }).ToList();
            var transmissionModels = transmissionViewModels.Cast<ITransmission>();
            return transmissionModels;
        }
 
        public ITransmission CreateTransmissionDomainModel()
        {
            var transmission = new TransmissionDomainModel(_transmissionRepository);
            return transmission;
        }

        public TransmissionDataModel GetTransmission(int transmissionId)
        {
            var transmission = _transmissionRepository
                .Get(t => t.TransmissionId == transmissionId
                );

            if (transmission == null)
            {
				var exception = new GenericNotFoundException<TransmissionService>("Transmission not found");
				_logger.LogError(exception, exception.Message);
			}

            return transmission;
        }
    }
}