using System.ComponentModel.DataAnnotations;

namespace eAuto.Data.Interfaces.DataModels
{
    public sealed class BodyTypeDataModel
    {
        [Required]
        [MaxLength(50)]
        public int BodyTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get;set;}

		#region Ctor
		public BodyTypeDataModel() { }

        public BodyTypeDataModel(string name)
        {
            Name = name;
        }
		#endregion
	}
}