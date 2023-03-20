namespace eAuto.Domain.Interfaces
{
    public interface IBrandService
    {
        IBrand GetBrandModel(int id);
        Task<IEnumerable<IBrand>> GetBrandModelsAsync();
		IBrand CreateBrandModel(string name);
	}
}
