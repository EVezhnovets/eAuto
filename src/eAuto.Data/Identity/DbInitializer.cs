using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using Microsoft.AspNetCore.Identity;

namespace eAuto.Data.Identity
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IdentityContext _db;

        public DbInitializer(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IdentityContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        public void Initialize()
        {
            if (!_roleManager.RoleExistsAsync(DataConstants.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(DataConstants.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(DataConstants.CustomerRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(DataConstants.EmployeeRole)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    PhoneNumber = "+3752911122234",
                    StreetAddress = "test 111 street",
                    City = "Minsk"
                },
                   "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");

                _userManager.AddToRoleAsync(user, DataConstants.AdminRole).GetAwaiter().GetResult();
            }
            return;
        }
    }
}