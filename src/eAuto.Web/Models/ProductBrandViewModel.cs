using System.ComponentModel;

namespace eAuto.Web.Models
{
	public sealed class ProductBrandViewModel
	{
		public int ProductBrandId { get; set; }
		[DisplayName("Product Brand")]
		public string? Name { get; set; }
		public ProductBrandViewModel() { }

		public ProductBrandViewModel(int productBrandId, string name)
		{
			ProductBrandId = productBrandId;
			Name = name;
		}
	}
}