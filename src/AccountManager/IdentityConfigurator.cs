﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AccountManager.Models;
using Microsoft.AspNetCore.Identity;

namespace AccountManager
{
	public static class IdentityConfigurator
	{
		public static void Configure(IServiceCollection services, string connectionstring)
		{
			services.AddDbContext<IdentityContext>(options =>
				options.UseSqlServer(connectionstring));
			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddDefaultTokenProviders()
				.AddDefaultUI()
				.AddEntityFrameworkStores<IdentityContext>();
		}
	}
}