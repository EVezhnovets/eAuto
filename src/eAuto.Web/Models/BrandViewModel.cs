using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eAuto.Web.Models
{
	public sealed class BrandViewModel
	{
		public int BrandId { get; set; }
		[DisplayName("Brand")]
		[Required]public string? Name { get; set; }
		public BrandViewModel()
		{
		}

		public BrandViewModel(int brandId, string name)
		{
			BrandId = brandId;
			Name = name;
		}
	}
}