using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eAuto.Data.Interfaces.DataModels
{
    public sealed class ModelDataModel
    {
        [Required]
        [MaxLength(50)]
        public int ModelId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        public int BrandId { get; set; }
        [ForeignKey("BrandId")] public BrandDataModel? Brand { get; set; }

        #region Ctor
        public ModelDataModel() { }
        public ModelDataModel(string name, int brandId)
        {
            Name = name;
            BrandId = brandId;
        }
		#endregion
	}
}