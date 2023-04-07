using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Models
{
	public sealed class CarsIndexViewModel
    {
		public IEnumerable<CarViewModel> CarVModels{ get; set; }

		public IEnumerable<SelectListItem>? Brands { get; set; }
		public int? BrandFilterApplied { get; set; }

		public IEnumerable<SelectListItem>? Models { get; set; }
		public int? ModelId { get; set; }
		public int? ModelFilterApplied { get; set; }

		public IEnumerable<SelectListItem>? Generations { get; set; }
		public int? GenerationFilterApplied { get; set; }

		public IEnumerable<SelectListItem>? BodyTypes { get; set; }
		public int? BodyTypeFilterApplied { get; set; }

		public IEnumerable<SelectListItem>? Engines { get; set; }
		public int? EngineFilterApplied { get; set; }

		public IEnumerable<SelectListItem>? DriveTypes { get; set; }
		public int? DriveTypeFilterApplied { get; set; }

		public IEnumerable<SelectListItem>? Transmissions { get; set; }
		public int? TransmissionFilterApplied { get; set; }
	}
}