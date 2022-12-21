using System.Text.Json;
using System.Transactions;
using Alba;
using Alba.Security;
using Botserve.MigrationApp.Migrations;
using BotServer.Data.Data;
using BotServer.Domain.ConfigModels;
using BotServer.Features.Features.Account.LoginCommand;
using BotServer.Features.Features.Account.RegistrationCommand;
using BotServer.Midlewares;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private static string jwtToken;

        [SetUp]
        public async Task Initialize()
        {
            host = await AlbaHost.For<global::Program>(o =>
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

            //arrange 
            // register test account
            RegistrationCommand registrationCommand = new RegistrationCommand();
            registrationCommand.Password = "Secret1_";
            registrationCommand.UserName = "User1";
            registrationCommand.Email = "user1@gmail.com";
            registrationCommand.ConfirmPass = "Secret1_";
            registrationCommand.VkEmail = "user1@gmail.com";

            await host.Scenario(_ =>
            {
                _.Post.Json(registrationCommand).ToUrl("/api/Account/Register");
            });

            LoginCommand loginCommand = new LoginCommand();
            loginCommand.Password = "Secret1_";
            loginCommand.UserName = "user1";
            var json = JsonSerializer.Serialize(loginCommand);

            //login
            var result = await host.Scenario(_ =>
            {
                _.Post.Json(loginCommand).ToUrl("/api/Account/Login");
                _.StatusCodeShouldBeOk();
            });

            jwtToken = result.ReadAsText();

        }
        [Test]
        [Category("Integration")]
        public async Task AccountController_Registration_shouldReturn200()
        {
            //arrange 
            RegistrationCommand registrationCommand = new RegistrationCommand();
            registrationCommand.Password = "Secret1_";
            registrationCommand.UserName = "User2";
            registrationCommand.Email = "user2@gmail.com";
            registrationCommand.ConfirmPass = "Secret1_";
            registrationCommand.VkEmail = "user2@gmail.com";

            //act
            await host.Scenario(_ =>
            {
                _.Post.Json(registrationCommand).ToUrl("/api/Account/Register");
                _.StatusCodeShouldBe(200);
            });
        }

        [Test]
        [Category("Integration")]
        public async Task AccountController_Login_ShoulReturn400()
        {
            //act
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
            //arrange
            //test login data
            LoginCommand loginCommand = new LoginCommand();
            loginCommand.Password = "Secret1_";
            loginCommand.UserName = "user1";
            var json = JsonSerializer.Serialize(loginCommand);  

            //act
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
            //act
            await host.Scenario(_ =>
            {
                _.Post.Json($$"""{ "UserName": "{{username}}",Password:{{password}},ConfirmPass:{{confirmpassword}}},email:{{email}}}""").ToUrl("/api/Account/Register");
                _.StatusCodeShouldBe(400);
            });
        }

        [Test]
        [Category("Integration")]

        public async Task AccountController_GetPersonalData_ShouldReturn401()
        {
            await host.Scenario(_ =>
            {
                _.Get.Url("/api/Account/GetPersonalData");
                _.StatusCodeShouldBe(401);
            });
        }

        [Test]
        [Category("Integration")]
        public async Task AccountController_GetPersonalData_ShouldReturn200()
        {
            var token = "Bearer " + jwtToken;
            await host.Scenario(_ =>
            {
                _.WithRequestHeader("Authorization", @token.Replace("\"",""));
                _.Get.Url("/api/Account/GetPersonalData");
                _.StatusCodeShouldBe(200);
            });
        }
    }
}