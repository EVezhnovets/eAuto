using System.ComponentModel;

namespace eAuto.Web.Models
{
	public sealed class DriveTypeViewModel
    {
		public int DriveTypeId { get; set; }
		[DisplayName("Drive Type")]
		public string? Name { get; set; }
		public DriveTypeViewModel()
		{
		}

		public DriveTypeViewModel(int driveTypeId, string name)
		{
            DriveTypeId = driveTypeId;
			Name = name;
		}
	}
}