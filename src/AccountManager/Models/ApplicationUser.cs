﻿using AccountManager.Interfaces.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AccountManager.Models
{
	public class ApplicationUser : IdentityUser, IApplicationUser
	{
		[Required]
		public string FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Phone { get; set; }
	}
}