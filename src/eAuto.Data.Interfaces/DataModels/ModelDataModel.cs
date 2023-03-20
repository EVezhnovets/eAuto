using System.ComponentModel.DataAnnotations;

namespace eAuto.Data.Interfaces.DataModels
{
    public class ModelDataModel
    {
        [Required]
        [MaxLength(50)]
        public int ModelId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}