using System.ComponentModel.DataAnnotations;

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
        [Required]
        [MaxLength(50)] 
        public string Brand { get; set; }
        //TODO make List of pictures
        [MaxLength(200)]
        public string? PictureUrl { get; set; }
        [Required] 
        [MaxLength(50)]
        public string Model { get; set; }
        [Required] 
        [MaxLength(50)]
        public string Generation { get; set; }
        [Required]
        [MaxLength(50)]
        public DateTime Year { get; set; }
        [Required]
        [MaxLength(50)]
        public DateTime DateArrival { get; set; }
        [Required] 
        [MaxLength(50)]
        public string BodyType { get; set; }
        [Required]
        [MaxLength(50)]
        //TODO enum?
        public string EngineType { get; set; }
        [Required]
        [MaxLength(50)]
        public int EngineCapacity { get; set; }
        [Required] 
        [MaxLength(50)]
        //TODO enum?
        public string DriveType { get; set; }
        [Required]
        [MaxLength(50)]
        //TODO enum?
        public string Transmission { get; set; }
        [Required] 
        [MaxLength(50)]
        public int Odometer { get; set; }
    }
}