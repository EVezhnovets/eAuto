using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;

namespace eAuto.Domain.Services
{
    public sealed class ProductBrandService : IProductBrandService
	{
        private readonly IRepository<ProductBrandDataModel> _productBrandRepository;
        private readonly IAppLogger<ProductBrandService> _logger;

        public ProductBrandService(
            IRepository<ProductBrandDataModel> productBrandRepository,
            IAppLogger<ProductBrandService> logger)
        {
            _productBrandRepository = productBrandRepository;
            _logger = logger;
        }

        public IProductBrand GetProductBrandModel(int id)
        {
            var productBrandDataModel = GetProductBrand(id);

            if (productBrandDataModel == null)
            {
				var exception = new GenericNotFoundException<ProductBrandService>("ProductBrand not found");
				_logger.LogError(exception, exception.Message);
			}

            var productBrandViewModel = 
                new ProductBrandDomainModel(productBrandDataModel!, _productBrandRepository);
            return productBrandViewModel;
        }

        public async Task<IEnumerable<IProductBrand>> GetProductBrandModelsAsync()
        {
            var productBrandEntities = await _productBrandRepository.GetAllAsync();

            if (productBrandEntities == null)
            {
				var exception = new GenericNotFoundException<ProductBrandService>("ProductBrand not found");
				_logger.LogError(exception, exception.Message);
			}

            var productBrandViewModels = productBrandEntities!
                .Select(i => new ProductBrandDomainModel()
                {
                    ProductBrandId = i.ProductBrandDataModelId,
                    Name = i.Name,
                }).ToList();
            var productBrandModels = productBrandViewModels.Cast<IProductBrand>();
            return productBrandModels;
        }
 
        public IProductBrand CreateProductBrandDomainModel()
        {
            var productBrand = new ProductBrandDomainModel(_productBrandRepository);
            return productBrand;
        }

        public ProductBrandDataModel GetProductBrand(int productBrandId)
        {
            var productBrand = _productBrandRepository
                .Get(t => t.ProductBrandDataModelId == productBrandId
                );

            if (productBrand == null)
            {
				var exception = new GenericNotFoundException<ProductBrandService>("ProductBrand not found");
				_logger.LogError(exception, exception.Message);
			}

            return productBrand!;
        }
    }
}