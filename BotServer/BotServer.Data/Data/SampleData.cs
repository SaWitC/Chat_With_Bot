using BotServer.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace BotServer.Data.Data
{
    public class SampleData
    {
        public static async Task Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                if (await roleManager.FindByNameAsync("user") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("user"));
                }

                if (await roleManager.FindByNameAsync("admin") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("admin"));
                }
            }
        }
    }
}
