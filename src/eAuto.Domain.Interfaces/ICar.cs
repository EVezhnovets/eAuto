namespace eAuto.Domain.Interfaces
{
    public interface ICar
    {
        int CarId { get; set; }
        decimal PriceInitial { get; set; }
        string PictureUrl { get; set; }
        DateTime Year { get; set; }
        DateTime DateArrival { get; set; }
        int Odometer { get; set; }
        public string Description { get; set; }

		int BrandId { get; set; }
        string Brand { get; set; }

        int ModelId { get; set; }
        string Model { get; set; }

		int GenerationId { get; set; }
		string Generation { get; set; }

        int BodyTypeId { get; set; }
        string BodyType { get; set; }

        int EngineId { get; set; }
        string Engine { get; set; }

        int DriveTypeId { get; set; }
        string DriveType { get; set; }

        int TransmissionId { get; set; }
        string Transmission { get; set; }

        void Save();
        void Delete();
    }
}