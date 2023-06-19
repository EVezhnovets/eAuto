using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Models
{
    public class OneToOneModel<View> where View : class
    {
        public View? ViewModel { get; set; }
        public IEnumerable<SelectListItem>? ModelFK { get; set; }
    }
}