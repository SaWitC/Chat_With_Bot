using BotServer.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Data.Data
{
    public class SampleDataForIntegrationTesting
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

            if (await userManager.FindByNameAsync("user1") == null)
            {
                var user = new User() { Id = Guid.Empty.ToString(), UserName = "user1", Email = "user1@gmailcom", VkEmail = "user1@gmailcom" };
                var result = await userManager.CreateAsync(user, "Secret1_");
            }
        }
    }
}
