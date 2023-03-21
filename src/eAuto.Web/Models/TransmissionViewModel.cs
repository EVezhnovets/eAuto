using System.ComponentModel;

namespace eAuto.Web.Models
{
	public sealed class TransmissionViewModel
    {
		public int TransmissionId { get; set; }
		[DisplayName("Transmission")]
		public string Name { get; set; }
		public TransmissionViewModel()
		{
		}

		public TransmissionViewModel(int transmissionId, string name)
		{
            TransmissionId = TransmissionId;
			Name = name;
		}
		//TODO PagedOptions(int skip, int take)
	}
}