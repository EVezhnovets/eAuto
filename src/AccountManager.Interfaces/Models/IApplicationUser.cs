using System.ComponentModel.DataAnnotations;

namespace AccountManager.Interfaces.Models
{
	public interface IApplicationUser
	{
		[Required]
		string FirstName { get; set; }
		string LastName { get; set; }
		string Phone { get; set; }
	}
}