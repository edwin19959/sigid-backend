//task80_edelacruz: Estructura de base de datos para usuarios de SIGID
using Microsoft.AspNetCore.Identity;
using SIGID.Core.Application.Enums;
using SIGID.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGID.Infrastructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //task80_edelacruz: Usuario administrador por defecto (admin@sigid.com)
            ApplicationUser defaultUser = new()
            {
                UserName = "admin",
                Email = "admin@sigid.com",
                Name = "Admin",
                LastName = "SIGID",
                EmailConfirmed = true,
                IdentificationNumber = "000-0000000-0",
                PhoneNumberConfirmed = true,
                IsActive = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Admin123!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }
            //task80_edelacruz: Fin usuario administrador por defecto
        }
    }
}
//task80_edelacruz: Fin estructura de base de datos para usuarios de SIGID
