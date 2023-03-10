using System.ComponentModel.DataAnnotations;

namespace eAuto.Data.Interfaces.DataModels
{
    public sealed class TransmissionDataModel
    {
        [Required]
        [MaxLength(50)]
        public int TransmissionId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get;set;}
    }
}