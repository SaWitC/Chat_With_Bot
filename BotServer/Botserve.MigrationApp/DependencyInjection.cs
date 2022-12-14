using BotServer.Data.Data;
using BotServer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Botserve.MigrationApp
{
    public class DependencyInjection
    {
        public static void AddMigraion(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, opt => opt.MigrationsAssembly("Botserve.MigrationApp"));
            });


            //for integration testing
            services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("testDb"));
            //DbContext

            //var options = new DbContextOptionsBuilder<AppDbContext>()
            //    //.UseInMemoryDatabase(databaseName: "testDb")
            //    .Options;

            //using (var context = new AppDbContext(options))
            //{
            //    var user = new User() { Email = "user1@sample.com",UserName = "user1",VkEmail = "user1@sample.com" };
            //    context.Users.Add(user);
            //    context.SaveChanges();
            //}
        }
    }
}
