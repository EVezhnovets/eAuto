using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Domain.Services
{
    public sealed class ModelService : IModelService
    {
        private readonly IRepository<ModelDataModel> _modelRepository;

        public ModelService(IRepository<ModelDataModel> modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public IModel GetModelModel(int id)
        {
            var modelDataModel = GetModel(id);

            if (modelDataModel == null)
            {
                throw new ModelNotFoundException();
            }

            var modelViewModel = new ModelDomainModel(modelDataModel, _modelRepository);
            return modelViewModel;
        }

        public async Task<IEnumerable<IModel>> GetModelModelsAsync()
        {
            var modelEntities = await _modelRepository
                .GetAllAsync(
                include: query => query
                .Include(m => m.Brand)
                );

            if (modelEntities == null)
            {
                throw new ModelNotFoundException();
            }

            var modelViewModels = modelEntities
                .Select(i => new ModelDomainModel()
                {
                    ModelId = i.ModelId,
                    Name = i.Name,
                    BrandId = i.BrandId,
                    Brand = i.Brand.Name.ToString()
                }).ToList();
            var modelModels = modelViewModels.Cast<IModel>();
            return modelModels;
        }

        public IModel CreateModelModel(string name)
        {
            var model = new ModelDomainModel(_modelRepository, name);
            return model;
        }

        public IModel CreateModelDomainModel()
        {
            var model = new ModelDomainModel(_modelRepository);
            return model;
        }

        public ModelDataModel GetModel(int modelId)
        {
            var model = _modelRepository
                .Get(
                    predicate: bt => bt.ModelId == modelId, include: query => query
                        .Include(m => m.Brand)
                );

            if (model == null)
            {
                throw new ModelNotFoundException();
            }
            return model;
        }
    }
}