using System.ComponentModel;

namespace eAuto.Web.Models
{
	public sealed class BrandViewModel
	{
		public int BrandId { get; set; }
		[DisplayName("Brand")]
		public string Name { get; set; }
		public BrandViewModel()
		{
		}

		public BrandViewModel(int brandId, string name)
		{
			BrandId = brandId;
			Name = name;
		}
		//TODO PagedOptions(int skip, int take)
	}
}
