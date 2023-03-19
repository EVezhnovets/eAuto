using eAuto.Data.Context;
using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Domain.Services
{
    public sealed class BodyTypeService : IBodyTypeService
    {
        private readonly IRepository<BodyTypeDataModel> _bodyTypeRepository;
        private readonly EAutoContext _eAutoContext;

        public BodyTypeService(IRepository<BodyTypeDataModel> bodyTypeRepository, EAutoContext eAutoContext)
        {
            _bodyTypeRepository = bodyTypeRepository;
            _eAutoContext = eAutoContext;
        }

        public IBodyType GetBodyTypeModel(int id)
        {
            var bodyTypeDataModel = GetBodyType(id);

            if (bodyTypeDataModel == null)
            {
                throw new BodyTypeNotFoundException();
            }

            var bodyTypeViewModel = new BodyTypeDomainModel(bodyTypeDataModel, _bodyTypeRepository);
            return bodyTypeViewModel;
        }

        public async Task<IEnumerable<IBodyType>?> GetBodyTypeModelsAsync()
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

        public async Task<IBodyType?> CreateBodyTypeModelAsync(IBodyType bodyType)
        {
            var bodyTypeDataModel = new BodyTypeDataModel()
            {
                BodyTypeId = bodyType.BodyTypeId,
                Name = bodyType.Name,
            };
            _bodyTypeRepository.Create(bodyTypeDataModel);
            return bodyType;
        }

        public async Task<IBodyType> CreateBodyTypeModelAsync(string name)
        {
            var bodyType = new BodyTypeDomainModel(_bodyTypeRepository, name);
            return bodyType;
        }

        public BodyTypeDataModel GetBodyType(int bodyTypeId)
        {
            var bodyType = _eAutoContext.BodyTypes
                .AsNoTracking()
                .FirstOrDefault(bt => bt.BodyTypeId == bodyTypeId);
            return bodyType;
        }
	}
}