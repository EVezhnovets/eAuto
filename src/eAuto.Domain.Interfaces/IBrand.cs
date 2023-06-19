namespace eAuto.Domain.Interfaces
{
    public interface IBrand
    {
        int BrandId { get; set; }
        string? Name { get; set; }
        void Save();
        void Delete();
    }
}
