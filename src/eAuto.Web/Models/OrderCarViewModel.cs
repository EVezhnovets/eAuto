using System.ComponentModel.DataAnnotations;

namespace eAuto.Web.Models
{
    public class OrderCarViewModel
    {
        public int Id { get; set; }
        [Required] public string? PhoneNumber { get; set; }
        [Required] public string? Name { get; set; }
        [Required] public string? Message { get; set; }
    }
}