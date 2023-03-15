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

        public BodyTypeService(IRepository<BodyTypeDataModel> bodyTypeRepository)
        {
            _bodyTypeRepository = bodyTypeRepository;
        }

        public async Task<IBodyType> GetBodyTypeViewModelAsync(int id)
        {
            var bodyTypeDataModel = await _bodyTypeRepository.GetByIdAsync(id);
            if(bodyTypeDataModel == null)
            {
                throw new BodyTypeNotFoundException();
            }

            var bodyTypeViewModel = new BodyTypeDomainModel(bodyTypeDataModel, _bodyTypeRepository);
            return bodyTypeViewModel;
        }

        public async Task<IEnumerable<IBodyType>?> GetBodyTypeViewModelsAsync()
        {
            var bodyTypeEntities = await _bodyTypeRepository.GetAllAsync();

            if(bodyTypeEntities == null)
            {
                throw new BodyTypeNotFoundException();
            }

            var bodyTypeViewModels = bodyTypeEntities
                .Select(i => new BodyTypeDomainModel()
                {
                    BodyTypeId = i.BodyTypeId,
                    Name = i.Name,
                }).ToList();
            var iBodyTypeModels = bodyTypeViewModels.Cast<IBodyType>();
            return iBodyTypeModels;
        }
    }
}
