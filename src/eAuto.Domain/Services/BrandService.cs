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
		private readonly IAppLogger<BrandService> _logger;

		public BrandService(
            IRepository<BrandDataModel> brandRepository, 
            IAppLogger<BrandService> logger)
		{
			_brandRepository = brandRepository;
			_logger = logger;
		}

		public IBrand GetBrandModel(int id)
        {
            var brandDataModel = GetBrand(id);

            if (brandDataModel == null)
            {
				var exception = new GenericNotFoundException<BrandService>("Brand not found");
				_logger.LogError(exception, exception.Message);
			}

            var brandViewModel = new BrandDomainModel(brandDataModel!, _brandRepository);
            return brandViewModel;
        }

        public async Task<IEnumerable<IBrand>> GetBrandModelsAsync()
        {
            var brandEntities = await _brandRepository.GetAllAsync();

            if(brandEntities == null)
            {
				var exception = new GenericNotFoundException<BrandService>("Brand not found");
				_logger.LogError(exception, exception.Message);
			}

            var brandViewModels = brandEntities!
                .Select(i => new BrandDomainModel()
                {
                    BrandId = i.BrandId,
                    Name = i.Name,
                }).ToList();
            var BrandModels = brandViewModels.Cast<IBrand>();
            return BrandModels;
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
				var exception = new GenericNotFoundException<BrandService>("Brand not found");
				_logger.LogError(exception, exception.Message);
			}
            return brand!;
        }
	}
}