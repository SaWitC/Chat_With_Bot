using BotServer.Data.Data;
using BotServer.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BotServer
{
    public class SetSampleData
    {
        public static async Task SetData(WebApplication app)
        {
            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var context = services.GetRequiredService<AppDbContext>();
                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }

                    var UserManager = services.GetRequiredService<UserManager<User>>();
                    var RoleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await SampleData.Initialize(UserManager, RoleManager);
                }
            }
            catch
            {

            }
        }
    }
}
