using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eAuto.Data.Interfaces.DataModels
{
    public class OrderDetailsDataModel
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        [ValidateNever]
        public OrderHeaderDataModel OrderHeader { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public MotorOilDataModel Product { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
    }
}