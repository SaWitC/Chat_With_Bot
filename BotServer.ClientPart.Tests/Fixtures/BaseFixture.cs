using Alba;
using BotServer.Data.Data;
using BotServer.Features.Features.Account.LoginCommand;
using BotServer.Features.Features.Account.RegistrationCommand;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace BotServer.ClientPart.Tests.Fixtures
{
    public class BaseFixture:IDisposable
    {
        public string JwtToken { get; private set; }
        public IAlbaHost Host { get; private set; }

        public async Task Init()
        {
            Host = await AlbaHost.For<global::Program>(o =>
            {
                o.UseEnvironment("Testing");
                o.ConfigureServices(services =>
                {
                    //AppDbContext
                    var dbContextDescriptor = services.FirstOrDefault(descriptor =>
                    descriptor.ServiceType == typeof(AppDbContext));
                    var dbContextOptionDescriptor = services.FirstOrDefault(descriptor =>
                    descriptor.ServiceType == typeof(DbContextOptions<AppDbContext>));

                    services.Remove(dbContextDescriptor);
                    services.Remove(dbContextOptionDescriptor);

                    services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("mycustombd"));

                    //Identity
                    services.AddIdentity<BotServer.Domain.Models.User, IdentityRole>()
                        .AddEntityFrameworkStores<AppDbContext>();
                });
            });
        }
        public async Task RegisterTestAccount_user1()
        {
            try
            {
                // register test account
                RegistrationCommand registrationCommand = new RegistrationCommand();
                registrationCommand.Password = "Secret1_";
                registrationCommand.UserName = "user1";
                registrationCommand.Email = "user1@gmail.com";
                registrationCommand.ConfirmPass = "Secret1_";
                registrationCommand.VkEmail = "user1@gmail.com";

                await Host.Scenario(_ => { _.Post.Json(registrationCommand).ToUrl("/api/Account/Register"); });
            }
            catch
            {

            }
        }

        public async Task RegisterAndLoginTestAccount_user1()
        {
            try
            {
                // register test account
                RegistrationCommand registrationCommand = new RegistrationCommand();
                registrationCommand.Password = "Secret1_";
                registrationCommand.UserName = "user1";
                registrationCommand.Email = "user1@gmail.com";
                registrationCommand.ConfirmPass = "Secret1_";
                registrationCommand.VkEmail = "user1@gmail.com";

                await Host.Scenario(_ => { _.Post.Json(registrationCommand).ToUrl("/api/Account/Register"); });
            }
            catch
            {

            }

            LoginCommand loginCommand = new LoginCommand();
            loginCommand.Password = "Secret1_";
            loginCommand.UserName = "user1";
            var json = JsonSerializer.Serialize(loginCommand);

            //login
            var result = await Host.Scenario(_ =>
            {
                _.Post.Json(loginCommand).ToUrl("/api/Account/Login");
            });

            JwtToken = result.ReadAsText();
        }
        public void Dispose()
        {
            Host.Dispose();
        }
    }
}
