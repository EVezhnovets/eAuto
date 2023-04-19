using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace eAuto.Web.Models
{
	public sealed class MotorOilViewModel
	{
		public int MotorOilId { get; set; }
		[DisplayName("Picture Url")]
		public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
		[DisplayName("Name")]
		public string Name { get; set; }
		public string Viscosity { get; set; }
		public string Composition { get; set; }
		public int Volume { get; set; }
		public int ProductBrandId { get; set; }
		[DisplayName("Product Brand")]
		[ValidateNever] public string ProductBrand { get; set; }

        #region Ctor
        public MotorOilViewModel() { }

        public MotorOilViewModel(
            int motorOilId, 
            string name, 
            string viscosity, 
            string composition, 
            int volume, 
            string productBrand)
        {
			MotorOilId = motorOilId;
            Name = name;
            Viscosity = viscosity;
            Composition = composition;
            Volume = volume;
            ProductBrand = productBrand;
        }
        #endregion
    }
}