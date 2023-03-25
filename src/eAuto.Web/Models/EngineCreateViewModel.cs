using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Models
{
	public sealed class EngineCreateViewModel
	{
		public EngineViewModel EngineVModel { get; set; }
		public IEnumerable<SelectListItem>? Brands { get; set; }
		public IEnumerable<SelectListItem>? Models { get; set; }
		public IEnumerable<SelectListItem>? Generations { get; set; }
    }
}