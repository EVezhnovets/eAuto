using System.ComponentModel.DataAnnotations;

namespace eAuto.Data.Interfaces.DataModels
{
    public class OrderCarDataModel
    {
        public int Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        public string ApplicationUserId { get; set; }
        public double? OrderTotal { get; set; }
        public string? OrderStatus { get; set; }
        public string? SessionId { get; set; }
        public string Message { get; set; }
    }
}