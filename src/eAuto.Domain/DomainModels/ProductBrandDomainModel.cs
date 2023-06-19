using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using ProductBrandDataM = eAuto.Data.Interfaces.DataModels.ProductBrandDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class ProductBrandDomainModel : IProductBrand
    {
        private readonly IRepository<ProductBrandDataM>? _productBrandRepository;
        private readonly string? _name;
        private readonly bool _isNew;

        public int ProductBrandId { get; set; }
        public string? Name { get; set; }

        public ProductBrandDomainModel() { }

        public ProductBrandDomainModel(IRepository<ProductBrandDataM> productBrandRepository)
        {
            _productBrandRepository = productBrandRepository;
            _isNew = true;
        }

        internal ProductBrandDomainModel(
            ProductBrandDataM productBrandDataModel,
            IRepository<ProductBrandDataM> productBrandRepository)
        {
            _productBrandRepository = productBrandRepository;
            _name = productBrandDataModel.Name;

            ProductBrandId = productBrandDataModel.ProductBrandDataModelId;
        }


		public void Save()
		{
			var productBrandDataModel = GetProductBrandDataModel();

            if (_isNew)
            {
                var result = _productBrandRepository!.Create(productBrandDataModel);
                ProductBrandId = result.ProductBrandDataModelId;
            }
            else
            {
                _productBrandRepository!.Update(productBrandDataModel);
            }
        }

		public void Delete()
		{
            var productBrandModel = GetProductBrandDataModel();
            if (!_isNew)
            {
                _productBrandRepository!.Delete(productBrandModel);
            }
		}

        private ProductBrandDataM GetProductBrandDataModel()
        {
            var productBrandDataM = new ProductBrandDataM
            {
                ProductBrandDataModelId = ProductBrandId,
                Name = Name
			};
            return productBrandDataM;
		}
	}
}