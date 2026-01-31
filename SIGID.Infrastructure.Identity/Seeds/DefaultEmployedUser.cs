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
            ApplicationUser defaultUser = new();
            defaultUser.UserName = "employeduser";
            defaultUser.Email = "employeduser@email.com";
            defaultUser.Name = "Juan";
            defaultUser.LastName = "Due";
            defaultUser.EmailConfirmed = true;
            defaultUser.IdentificationNumber = "002-1345678-9";
            defaultUser.PhoneNumberConfirmed = true;

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
