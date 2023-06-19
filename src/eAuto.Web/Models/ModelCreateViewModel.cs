using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Models
{
	public sealed class ModelCreateViewModel
	{
		public ModelViewModel? ModelVModel { get; set; }
		public IEnumerable<SelectListItem>? Brands { get; set; }
    }
}