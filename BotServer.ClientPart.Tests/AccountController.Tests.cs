using Alba;
using BotServer.ClientPart.Tests.Fixtures;
using BotServer.Data.Data;
using BotServer.Features.Features.Account.LoginCommand;
using BotServer.Features.Features.Account.RegistrationCommand;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace BotServer.Clietn.Tests
{
    public class AccountControllerIntegrationTests
    {
        private BaseFixture fixture = new();
        
        
        [SetUp]
        public async Task Initialize()
        {
            fixture.Initialize();
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
            await fixture.Host.Scenario(_ =>
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
            await fixture.Host.Scenario(_ =>
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
            await fixture.Host.Scenario(_ =>
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
            await fixture.Host.Scenario(_ =>
            {
                _.Post.Json($$"""{ "UserName": "{{username}}",Password:{{password}},ConfirmPass:{{confirmpassword}}},email:{{email}}}""").ToUrl("/api/Account/Register");
                _.StatusCodeShouldBe(400);
            });
        }

        [Test]
        [Category("Integration")]

        public async Task AccountController_GetPersonalData_ShouldReturn401()
        {
            await fixture.Host.Scenario(_ =>
            {
                _.Get.Url("/api/Account/GetPersonalData");
                _.StatusCodeShouldBe(401);
            });
        }

        [Test]
        [Category("Integration")]
        public async Task AccountController_GetPersonalData_ShouldReturn200()
        {
            var token = "Bearer " + fixture.JwtToken;
            await fixture.Host.Scenario(_ =>
            {
                _.WithRequestHeader("Authorization", @token.Replace("\"",""));
                _.Get.Url("/api/Account/GetPersonalData");
                _.StatusCodeShouldBe(200);
            });
        }
    }
}