using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Domain.Services
{
    public sealed class TransmissionService : ITransmissionService
	{
        private readonly IRepository<TransmissionDataModel> _transmissionRepository;

        public TransmissionService(IRepository<TransmissionDataModel> transmissionRepository)
        {
            _transmissionRepository = transmissionRepository;
        }

        public ITransmission GetTransmissionModel(int id)
        {
            var transmissionDataModel = GetTransmission(id);

            if (transmissionDataModel == null)
            {
                throw new TransmissionNotFoundException();
            }

            var transmissionViewModel = new TransmissionDomainModel(transmissionDataModel, _transmissionRepository);
            return transmissionViewModel;
        }

        public async Task<IEnumerable<ITransmission>> GetTransmissionModelsAsync()
        {
            var transmissionEntities = await _transmissionRepository.GetAllAsync();

            if (transmissionEntities == null)
            {
                throw new TransmissionNotFoundException();
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
                throw new TransmissionNotFoundException();
            }
            return transmission;
        }
    }
}