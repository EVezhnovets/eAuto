using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace eAuto.Web.Models
{
	public sealed class EngineViewModel
	{
		public int EngineId { get; set; }
		[DisplayName("Engine Identification Name ")]
		public string IdentificationName { get; set; }
		public string Type { get; set; }
		public int Capacity { get; set; }
		public int Power { get; set; }
		public string Description { get; set; }

		public int BrandId { get; set; }
        [ValidateNever] public string Brand { get; set; }

        public int ModelId { get; set; }
        [ValidateNever] public string Model { get; set; }

        public int GenerationId { get; set; }
		[ValidateNever] public string Generation { get; set; }

        #region Ctor
        public EngineViewModel()
        {
        }

        public EngineViewModel(
            int engineId, 
            string identificationName, 
            string type,
            int capacity,
            int power,
            string description,
            string brand, 
            string model,
            string generation)
        {
            EngineId = engineId;
            IdentificationName = identificationName;
            Type = type;
            Capacity = capacity;
            Power = power;
            Description = description;
            Brand = brand;
            Model = model;
            Generation = generation;
        }
        #endregion
    }
}