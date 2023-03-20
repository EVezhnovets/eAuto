using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eAuto.Data.Interfaces.DataModels
{
    public sealed class GenerationDataModel
    {
        [Required]
        [MaxLength(50)]
        public int GenerationId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int BrandId { get; set; }
        [ForeignKey("BrandId")] public BrandDataModel Brand { get; set; }

        [Required]
        public int ModelId { get; set; }
        [ForeignKey("ModelId")] public ModelDataModel Model { get; set; }
    }
}