using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eAuto.Data.Interfaces.DataModels
{
    public sealed class EngineDataModel
    {
        [Required]
        [MaxLength(50)]
        public int EngineTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string IdentificationName { get;set;}

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [MaxLength(50)]
        public int Capacity { get; set; }

        [Required]
        [MaxLength(50)]
        public int Power { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public int BrandId { get; set; }
        [ForeignKey("BrandId")] public BrandDataModel Brand { get; set; }

        [Required]
        [MaxLength(50)]
        public int ModelId { get; set; }
        [ForeignKey("ModelId")] public ModelDataModel Model { get; set; }

        [Required]
        [MaxLength(50)]
        public int GenerationId { get; set; }
        [ForeignKey("GenerationId")] public GenerationDataModel Generation { get; set; }
    }
}