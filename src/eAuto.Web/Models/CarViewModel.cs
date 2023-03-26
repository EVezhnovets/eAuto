﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAuto.Web.Models
{
	public sealed class CarViewModel
	{
		public int CarId { get; set; }
        public decimal PriceInitial { get; set; }
        public string PictureUrl { get; set; }
        public DateTime Year { get; set; }
        public DateTime DateArrival { get; set; }
        public int Odometer { get; set; }
        public string Description { get; set; }

		public int BrandId { get; set; }
        [ValidateNever] public string Brand { get; set; }

        public int ModelId { get; set; }
        [ValidateNever] public string Model { get; set; }

        public int GenerationId { get; set; }
		[ValidateNever] public string Generation { get; set; }
        public int BodyTypeId { get; set; }
        [ValidateNever] public string BodyType { get; set; }

        public int EngineId { get; set; }
        [ValidateNever] public string Engine { get; set; }

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
            string brand, 
            string model,
            string generation,
            string bodyType,
            string engine,
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
            Brand = brand;
            Model = model;
            Generation = generation;
            BodyType = bodyType;
            Engine = engine;
            DriveType = driveType;
            Transmission = transmission;
        }
        #endregion
    }
}