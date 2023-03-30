using AccountManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountManager
{
	public class IdentityContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
	}
}