﻿using System.ComponentModel.DataAnnotations;
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
		public string Viscosity { get; set; }

		[Required]
		[MaxLength(50)]
		public string Composition { get; set; }

		[Required]
		[MaxLength(50)]
		public int Volume { get; set;}

		[Required]
		public int ProductBrandDataModelId { get; set; }
		[ForeignKey("ProductBrandDataModelId")] public ProductBrandDataModel ProductBrand { get; set; }
	}
}