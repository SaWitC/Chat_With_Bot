using System.Text.Json;
using System.Transactions;
using Alba.Security;
using Botserve.MigrationApp.Migrations;
using BotServer.Data.Data;
using BotServer.Features.Features.Account.LoginCommand;
using BotServer.Features.Features.Account.RegistrationCommand;
using BotServer.Midlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SwaggerCodegen;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;

namespace BotServer.Clietn.Tests
{
    public class AccountControllerIntegrationTests
    {
        private IAlbaHost host;

        [SetUp]
        public async Task Initialize()
        {
            host = await AlbaHost.For<global::Program>(o =>
            {
                o.ConfigureServices(services =>
                {
                    var dbContextDescriptor = services.FirstOrDefault(descriptor =>
                    descriptor.ServiceType == typeof(AppDbContext));
                    var dbContextOptionDescriptor = services.FirstOrDefault(descriptor =>
                    descriptor.ServiceType == typeof(DbContextOptions<AppDbContext>));

                    services.Remove(dbContextDescriptor);
                    services.Remove(dbContextOptionDescriptor);

                    services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("mycustombd"));
                    services.AddIdentity<User, IdentityRole>()
                        .AddEntityFrameworkStores<AppDbContext>();
                });
            });
        }
        [Fact]
        public async Task AccountController_Registration_shouldReturn200()
        {
            await using var host = await AlbaHost.For<global::Program>(x =>
            {
            });
            RegistrationCommand registrationCommand = new RegistrationCommand();
            registrationCommand.Password = "Secret1_";
            registrationCommand.UserName = "User1";
            registrationCommand.Email = "user1@gmail.com";
            registrationCommand.ConfirmPass = "Secret1_";

            await host.Scenario(_ =>
            {
                _.Post.Json(registrationCommand).ToUrl("/api/Account/Register");
                _.StatusCodeShouldBe(200);
            });
        }

        [Fact]
        [Category("Integration")]
        public async Task AccountController_Login_ShoulReturn400()
        {
            await using var host = await AlbaHost.For<global::Program>();

            await host.Scenario(_ =>
            {
                _.Get.Url("/api/Account/Login");
                _.Post.Json("""{ "userName": "string","password": "string"}""");
                _.StatusCodeShouldBe(400);
            });
        }

        [Test]
        [Category("Integration")]
        public async Task AccountController_Login_ShouldReturn200()
        {
            await using var host = await AlbaHost.For<global::Program>();

            LoginCommand loginCommand = new LoginCommand();
            loginCommand.Password = "Secret1_";
            loginCommand.UserName = "user1";
            var json=JsonSerializer.Serialize(loginCommand);

            await host.Scenario(_ =>
            {
                _.Post.Json(loginCommand).ToUrl("/api/Account/Login");
                _.StatusCodeShouldBeOk();
            });
        }
        [TestCase("user1", "user1@gmail.com", "Secret1_", "Secret1_")]
        [TestCase("1", "1", "1", "1")]
        [TestCase("user1", "user1@gmail.com", "Secret2_", "Secret1_")]
        [TestCase("user1", "user1@.com", "Secret1_", "Secret1_")]
        [TestCase("user1", "user1@gmail.com", "Secret1", "Secret1")]
        [TestCase("user1", "user1@gmail.com", "Secret_", "Secret_")]
        [Category("Integration")]

        public async Task AccountController_Register_ShoulReturn400(string username, string email, string password, string confirmpassword)
        {
            await using var host = await AlbaHost.For<global::Program>();
            await host.Scenario(_ =>
            {
                _.Get.Url("/api/Account/Register");
                _.Post.Json($$"""{ "UserName": "{{username}}",Password:{{password}},ConfirmPass:{{confirmpassword}}},email:{{email}}}""");
                _.StatusCodeShouldBe(400);
            });
        }
    }
}