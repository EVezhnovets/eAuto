using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace eAuto.Web.Models
{
	public sealed class GenerationViewModel
	{
		public int GenerationId { get; set; }
		[DisplayName("Generation")]
		public string Name { get; set; }

        public int BrandId { get; set; }
        [ValidateNever] public string Brand { get; set; }

        public int ModelId { get; set; }
        [ValidateNever] public string Model { get; set; }
        #region Ctor
        public GenerationViewModel()
        {
        }

        public GenerationViewModel(int generationId, string name, string brand, string model)
        {
            GenerationId = generationId;
            Name = name;
            Brand = brand;
            Model = model;
        }
        #endregion
        //TODO PagedOptions(int skip, int take)
    }
}