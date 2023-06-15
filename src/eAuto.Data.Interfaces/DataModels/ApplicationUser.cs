using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace eAuto.Data.Interfaces.DataModels
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public string FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Phone { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
    }
}