using System.ComponentModel;

namespace eAuto.Web.Models
{
	public sealed class BodyTypeViewModel
	{
		public int BodyTypeId { get; set; }
		[DisplayName("Body Type")]
		public string Name { get; set; }
		public BodyTypeViewModel()
		{
		}

		public BodyTypeViewModel(int bodyTypeId, string name)
		{
			BodyTypeId = bodyTypeId;
			Name = name;
		}
	}
}
