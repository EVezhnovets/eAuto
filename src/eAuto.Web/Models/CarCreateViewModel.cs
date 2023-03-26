using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Models
{
	public sealed class CarCreateViewModel
	{
		public CarViewModel CarVModel { get; set; }
		public IEnumerable<SelectListItem>? Brands { get; set; }
		public IEnumerable<SelectListItem>? Models { get; set; }
		public IEnumerable<SelectListItem>? Generations { get; set; }
		public IEnumerable<SelectListItem>? BodyTypes { get; set; }
		public IEnumerable<SelectListItem>? Engines { get; set; }
		public IEnumerable<SelectListItem>? DriveTypes { get; set; }
		public IEnumerable<SelectListItem>? Transmissions { get; set; }
    }
}