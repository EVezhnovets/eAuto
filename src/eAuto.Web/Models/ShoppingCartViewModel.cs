using eAuto.Data.Interfaces.DataModels;
using System.ComponentModel.DataAnnotations;

namespace eAuto.Web.Models
{
    public class ShoppingCartViewModel
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public MotorOilViewModel? Product { get; set; }

        [Range(1, 50, ErrorMessage = "Please enter value between 1 and 50")]
        public int Count { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public double Price { get; set; }
    }
}