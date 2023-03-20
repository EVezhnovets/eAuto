using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;

namespace eAuto.Domain.Services
{
    public sealed class BrandService : IBrandService
    {
        private readonly IRepository<BrandDataModel> _brandRepository;

        public BrandService(IRepository<BrandDataModel> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IBrand GetBrandModel(int id)
        {
            var brandDataModel = GetBrand(id);

            if (brandDataModel == null)
            {
                throw new BrandNotFoundException();
            }

            var brandViewModel = new BrandDomainModel(brandDataModel, _brandRepository);
            return brandViewModel;
        }

        public async Task<IEnumerable<IBrand>> GetBrandModelsAsync()
        {
            var brandEntities = await _brandRepository.GetAllAsync();

            if(brandEntities == null)
            {
                throw new BrandNotFoundException();
            }

            var brandViewModels = brandEntities
                .Select(i => new BrandDomainModel()
                {
                    BrandId = i.BrandId,
                    Name = i.Name,
                }).ToList();
            var iBrandModels = brandViewModels.Cast<IBrand>();
            return iBrandModels;
        }

        public IBrand CreateBrandModel(string name)
        {
            var brand = new BrandDomainModel(_brandRepository, name);
            return brand;
        }

        public BrandDataModel GetBrand(int brandId)
        {
            var brand = _brandRepository
                .Get(bt => bt.BrandId == brandId);

            if(brand == null)
            {
                throw new BrandNotFoundException();
            }
            return brand;
        }
	}
}