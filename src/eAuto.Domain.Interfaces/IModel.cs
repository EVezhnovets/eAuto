namespace eAuto.Domain.Interfaces
{
    public interface IModel
    {
        int ModelId { get; set; }
        string Name { get; set; }
        int BrandId { get; set; }
        string Brand { get; set; }
        void Save();
        void Delete();
    }
}
