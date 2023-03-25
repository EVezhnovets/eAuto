using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eAuto.Data.Interfaces.DataModels
{
    public sealed class CarDataModel
    {
        [Required]
        [MaxLength(100)]
        public int CarId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public decimal PriceInitial{ get; set; }

        //TODO make List of pictures
        [MaxLength(200)]
        public string? PictureUrl { get; set; }

        [Required]
        [MaxLength(50)]
        public DateTime Year { get; set; }

        [Required]
        [MaxLength(50)]
        public DateTime DateArrival { get; set; }

        [Required]
        [MaxLength(50)]
        public int Odometer { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]public BrandDataModel Brand { get; set; }
        
        [Required] 
        [MaxLength(50)]
        public int ModelId { get; set; }
        [ForeignKey("ModelId")]public ModelDataModel Model { get; set; }

        [Required] 
        [MaxLength(50)]
        public int GenerationId { get; set; }
        [ForeignKey("GenerationId")] public GenerationDataModel Generation { get; set; }
        
        [Required] 
        [MaxLength(50)]
        public int BodyTypeId { get; set; }
        [ForeignKey("BodyTypeId")] public BodyTypeDataModel BodyType { get; set; }

        [Required]
        [MaxLength(50)]
        public int EngineId { get; set; }
        [ForeignKey("EngineId")]public EngineDataModel Engine { get; set; }

        [Required] 
        [MaxLength(50)]
        public int DriveTypeId { get; set; }
        [ForeignKey("DriveTypeId")]public DriveTypeDataModel DriveType { get; set; }

        [Required]
        [MaxLength(50)]
        public int TransmissionId { get; set; }
        [ForeignKey("TransmissionId")]public TransmissionDataModel Transmission { get; set; }
    }
}