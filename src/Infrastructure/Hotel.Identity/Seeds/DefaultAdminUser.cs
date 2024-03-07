using Hotel.Core.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Hotel.Infrastructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager)
        {
            var defaultAdmin = new IdentityUser
            {
                UserName = "agente",
                Email = "agrentehotel@mail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultAdmin.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultAdmin.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdmin, "123Pa$word");
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
                }
            }
        }
    }
}
