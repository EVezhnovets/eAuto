using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eAuto.Data.Interfaces.DataModels
{
	public sealed class MotorOilDataModel
	{
		[Required]
		[MaxLength(50)]
		public int MotorOilDataModelId { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

        [MaxLength(200)]
        public string? PictureUrl { get; set; }

        [Required]
        [MaxLength(50)]
        public decimal Price { get; set; }
        [Required]
		[MaxLength(50)]
		public string Viscosity { get; set; }

		[Required]
		[MaxLength(50)]
		public string Composition { get; set; }

		[Required]
		[MaxLength(50)]
		public int Volume { get; set;}

		[Required]
		public int ProductBrandId { get; set; }
		[ForeignKey("ProductBrandId")] 
		public ProductBrandDataModel ProductBrand { get; set; }

		#region Ctor
		public MotorOilDataModel() { }
		public MotorOilDataModel(
			string name,
			string? pictureUrl,
			decimal price,
			string viscosity, 
			string composition, 
			int volume,
			int productBrandId)
		{
			Name = name;
			PictureUrl = pictureUrl;
			Price = price;
			Viscosity = viscosity;
			Composition = composition;
			Volume = volume;
			ProductBrandId = productBrandId;
		}
		#endregion
	}
}