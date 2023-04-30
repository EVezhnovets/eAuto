using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eAuto.Data.Interfaces.DataModels;


namespace eAuto.Data.Identity
{
	public class IdentityContext : IdentityDbContext
	{
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
	}
}