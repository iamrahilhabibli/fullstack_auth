using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiOnionArchitecture.Domain.Entities.Identity;
using WebApiOnionArchitecture.Domain.Enums;

namespace WebApiOnionArchitecture.Persistence.Contexts
{
    public class AppDbContextInitialiser
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AppDbContextInitialiser(AppDbContext context,
                                       UserManager<AppUser> userManager,
                                       RoleManager<IdentityRole> roleManager,
                                       IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task InitialiseAsync()
        {
            await _context.Database.MigrateAsync();

        }
        public async Task RoleSeedAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Role)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new() { Name = role.ToString() });
                }
            }
        }
        public async Task UserSeedAsync()
        {
            AppUser appUser = new()
            {
                UserName = _configuration["SuperAdminSetting:username"],
                Email = _configuration["SuperAdminSetting:email"]
            };
            await _userManager.CreateAsync(appUser, _configuration["SuperAdminSetting:password"]);
            await _userManager.AddToRoleAsync(appUser, Role.SuperAdmin.ToString());
        }
    }
}
