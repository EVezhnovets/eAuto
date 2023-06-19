using System.ComponentModel.DataAnnotations;

namespace eAuto.Data.Interfaces.DataModels
{
    public sealed class ProductBrandDataModel
	{
        [Required]
        [MaxLength(50)]
        public int ProductBrandDataModelId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get;set;}

        #region Ctor
        public ProductBrandDataModel() { }
        public ProductBrandDataModel(string name)
        {
            Name = name;
        }
		#endregion
	}
}