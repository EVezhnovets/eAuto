namespace eAuto.Web.Models
{
    public class ShoppingCartIndexViewModel
    {
        public IEnumerable<ShoppingCartViewModel>? ShoppingCartList { get; set; }
        public OrderHeaderViewModel? OrderHeader { get;set; }
    }
}