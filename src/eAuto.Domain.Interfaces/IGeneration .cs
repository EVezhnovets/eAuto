namespace eAuto.Domain.Interfaces
{
    public interface IGeneration
    {
        int GenerationId { get; set; }
        string Name { get; set; }
        int BrandId { get; set; }
        string Brand { get; set; }
        int ModelId { get; set; }
        string Model { get; set; }
        void Save();
        void Delete();
    }
}
