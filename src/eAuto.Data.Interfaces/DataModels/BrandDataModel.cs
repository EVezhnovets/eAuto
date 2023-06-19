using System.ComponentModel.DataAnnotations;

namespace eAuto.Data.Interfaces.DataModels
{
    public sealed class BrandDataModel
    {
        [Required]
        [MaxLength(50)]
        public int BrandId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get;set;}

        #region Ctor
        public BrandDataModel() { }
        public BrandDataModel(string name)
        {
            Name = name;
        }
		#endregion
	}
}