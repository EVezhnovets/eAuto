namespace eAuto.Domain.Interfaces
{
    public interface IProductBrand
	{
        int ProductBrandId { get; set; }
        string? Name { get; set; }
		void Save();
        void Delete();
    }
}