//task80_edelacruz: Estructura de base de datos para usuarios de SIGID
using Microsoft.AspNetCore.Identity;
using SIGID.Core.Application.Enums;
using SIGID.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGID.Infrastructure.Identity.Seeds
{
    public static class DefaultEmployedUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //task80_edelacruz: Correccion de inicializador de objeto para propiedades required
            ApplicationUser defaultUser = new()
            {
                UserName = "employeduser",
                Email = "employeduser@email.com",
                Name = "Juan",
                LastName = "Due",
                EmailConfirmed = true,
                IdentificationNumber = "002-1345678-9",
                PhoneNumberConfirmed = true
            };
            //task80_edelacruz: Fin correccion

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Employed.ToString());
                }
            }
        }
    }
}
