namespace eAuto.Domain.Interfaces
{
    public interface IEngine
    {
        int EngineId { get; set; }
        string IdentificationName { get; set; }
		public string Type { get; set; }
		public int Capacity { get; set; }
		public int Power { get; set; }
		public string Description { get; set; }
		int BrandId { get; set; }
        string Brand { get; set; }
        int ModelId { get; set; }
        string Model { get; set; }
		int GenerationId { get; set; }
		string Generation { get; set; }
		void Save();
        void Delete();
    }
}