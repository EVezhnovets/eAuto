using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAuto.Data.Interfaces.DataModels
{
	public class ShoppingCartDataModel
	{
		public int ShoppingCartId { get; set; }
		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		[ValidateNever]
		public MotorOilDataModel Product { get; set; }

		[Range(1, 1000, ErrorMessage = "Please enter value between 1 and 1000")]
		public int Count { get; set; }
		public string ApplicationUserId { get; set; }

		[NotMapped]
		public double Price { get; set; }
	}
}