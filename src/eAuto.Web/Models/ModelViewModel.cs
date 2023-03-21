using eAuto.Data.Interfaces.DataModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace eAuto.Web.Models
{
	public sealed class ModelViewModel
	{
		public int ModelId { get; set; }
		[DisplayName("Model")]
		public string Name { get; set; }

        public int BrandId { get; set; }
        [ValidateNever] public string Brand { get; set; }
        #region Ctor
        public ModelViewModel()
        {
        }

        public ModelViewModel(int modelId, string name, string brand)
        {
            ModelId = modelId;
            Name = name;
            Brand = brand;
        }
        #endregion
        //TODO PagedOptions(int skip, int take)
    }
}
