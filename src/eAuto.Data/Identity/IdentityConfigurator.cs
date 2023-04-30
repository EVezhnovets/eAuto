using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using eAuto.Data.Interfaces.DataModels;

namespace eAuto.Data.Identity
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