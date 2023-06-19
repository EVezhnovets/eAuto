using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Models
{
	public sealed class MotorOilCreateViewModel
	{
		public MotorOilViewModel? MotorOilVModel { get; set; }
		public IEnumerable<SelectListItem>? ProductBrands { get; set; }
    }
}