using Hotel.Core.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Hotel.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Traveler.ToString()));
        }
    }
}
