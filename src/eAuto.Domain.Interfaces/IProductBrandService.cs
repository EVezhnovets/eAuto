namespace eAuto.Domain.Interfaces
{
    public interface IProductBrandService
    {
		IProductBrand GetProductBrandModel(int id);
        Task<IEnumerable<IProductBrand>> GetProductBrandModelsAsync();
        IProductBrand CreateProductBrandDomainModel();
    }
}