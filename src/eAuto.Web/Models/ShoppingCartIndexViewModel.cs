namespace eAuto.Web.Models
{
    public class ShoppingCartIndexViewModel
    {
        public IEnumerable<ShoppingCartViewModel> ShoppingCartList { get; set; }
        public double CartTotal { get;set; }
    }
}