using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Models
{
	public sealed class GenerationCreateViewModel
	{
		public GenerationViewModel GenerationVModel { get; set; }
		public IEnumerable<SelectListItem>? Brands { get; set; }
		public IEnumerable<SelectListItem>? Models { get; set; }
    }
}