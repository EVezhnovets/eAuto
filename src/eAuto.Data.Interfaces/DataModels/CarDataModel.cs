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
        [MaxLength(500)]
        public string Description { get; set; }

		[MaxLength(50)]
        public string? EngineIdentificationName { get; set; }

		[Required]
		[MaxLength(50)]
        public string EngineFuelType { get; set; }

		[MaxLength(50)]
        public string? EngineCapacity { get; set; }

		[MaxLength(50)]
        public int? EnginePower { get; set; }

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
        public int DriveTypeId { get; set; }
        [ForeignKey("DriveTypeId")]public DriveTypeDataModel DriveType { get; set; }

        [Required]
        [MaxLength(50)]
        public int TransmissionId { get; set; }
        [ForeignKey("TransmissionId")]public TransmissionDataModel Transmission { get; set; }

        #region Ctor
        public CarDataModel() { }

        public CarDataModel(
            decimal price,
            string pictureUrl,
            DateTime year,
            DateTime dateArrival,
            int odometer,
            string desrciption,
			string? engineIdentificationName,
			string engineFuelType,
			string? engineCapacity,
			int? enginePower,
			int brandId,
			int modelId,
			int generationId,
			int bodyTypeId,
			int driveTypeId,
			int transmissionId
			)
        {
            PriceInitial = price;
            PictureUrl = pictureUrl;
            Year = year;
            DateArrival = dateArrival;
            Odometer = odometer;
            Description = desrciption;
            EngineIdentificationName = engineIdentificationName;
            EngineFuelType = engineFuelType;
            EngineCapacity = engineCapacity;
            EnginePower = enginePower;
            BrandId = brandId;
            ModelId = modelId;
            GenerationId = generationId;
            BodyTypeId = bodyTypeId;
            DriveTypeId = driveTypeId;
            TransmissionId = transmissionId;
        }
		#endregion
	}
}