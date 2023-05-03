using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace eAuto.Web.Models
{
    public sealed class CarViewModel
    {
        public int CarId { get; set; }
        public decimal PriceInitial { get; set; }
        public string? PictureUrl { get; set; }
		[DataType("month")] public DateTime Year { get; set; } = DateTime.Now;
        [DataType("month")] public DateTime DateArrival { get; set; } = DateTime.Now;
		public int Odometer { get; set; }
        public string Description { get; set; }
		public string? EngineIdentificationName { get; set; }
		public int EngineFuelTypeId { get; set; }
		public string EngineFuelType { get; set; }
		public string? EngineCapacity { get; set; }
		public int? EnginePower { get; set; }

		public int BrandId { get; set; }
        [ValidateNever] public string Brand { get; set; }

        public int ModelId { get; set; }
        [ValidateNever] public string Model { get; set; }

        public int GenerationId { get; set; }
		[ValidateNever] public string Generation { get; set; }
        public int BodyTypeId { get; set; }
        [ValidateNever] public string BodyType { get; set; }

        public int DriveTypeId { get; set; }
        [ValidateNever] public string DriveType { get; set; }

        public int TransmissionId { get; set; }
        [ValidateNever] public string Transmission { get; set; }

        #region Ctor
        public CarViewModel()
        {
        }

        public CarViewModel(
            int carId, 
            decimal priceInitial,
            string picture,
            DateTime year,
            DateTime dateArrival,
            int odometer,
            string description,
			string? engineIdentificationName,
			string? engineCapacity,
			string engineFuelType,
			int engineFuelTypeId,
			int? enginePower,
			string brand, 
            string model,
            string generation,
            string bodyType,
            string driveType,
            string transmission)
        {
            CarId = carId;
            PriceInitial = priceInitial;
            PictureUrl = picture;
            Year = year;
            DateArrival = dateArrival;
            Odometer = odometer;
            Description = description;
            EngineIdentificationName = engineIdentificationName;
            EngineFuelType = engineFuelType;
            EngineFuelTypeId = engineFuelTypeId;
            EngineCapacity = engineCapacity;
            EnginePower = enginePower;
            Brand = brand;
            Model = model;
            Generation = generation;
            BodyType = bodyType;
            DriveType = driveType;
            Transmission = transmission;
        }
        #endregion
    }
}