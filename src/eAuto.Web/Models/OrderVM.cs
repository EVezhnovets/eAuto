using eAuto.Data.Interfaces.DataModels;

namespace eAuto.Web.Models
{
    public class OrderVM
    {
        public OrderHeaderViewModel? OrderHeader { get; set; }
        public IEnumerable<OrderDetailsDataModel>? OrderDetails { get; set; }
    } 
}