using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eAuto.Web.Models
{
	public sealed class TransmissionViewModel
	{
		public int TransmissionId { get; set; }
		[DisplayName("Transmission")]
		[Required]public string? Name { get; set; }
		public TransmissionViewModel()
		{
		}

		public TransmissionViewModel(int transmissionId, string name)
		{
			TransmissionId = transmissionId;
			Name = name;
		}
	}
}