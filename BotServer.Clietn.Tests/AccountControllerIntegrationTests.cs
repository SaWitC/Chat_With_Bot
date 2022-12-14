using System.Text.Json;
using System.Transactions;
using Alba.Security;
using Botserve.MigrationApp.Migrations;
using BotServer.Features.Features.Account.LoginCommand;
using BotServer.Features.Features.Account.RegistrationCommand;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;
using NUnit.Framework;
using VkNet.Enums.SafetyEnums;

namespace BotServer.Clietn.Tests
{
    public class AccountControllerIntegrationTests
    {
        //private readonly TransactionScope _scope;
        public AccountControllerIntegrationTests()
        {
            //var scope = new TransactionScope();
            //_scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
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

        [Fact]
        //[Category("Integration")]
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
        //[TestCase("user1","user1@gmail.com","Secret1_","Secret1_")]
        [TestCase("1", "1", "1", "1")]
        [TestCase("user1", "user1@gmail.com", "Secret2_", "Secret1_")]
        [TestCase("user1", "user1@.com", "Secret1_", "Secret1_")]
        [TestCase("user1", "user1@gmail.com", "Secret1", "Secret1")]
        [TestCase("user1", "user1@gmail.com", "Secret1_", "Secret1_")]
        [TestCase("user1", "user1@gmail.com", "Secret_", "Secret_")]
        [Category("Integration")]


        public async Task AccountController_Register_ShoulReturn400(string username,string email, string password,string confirmpassword)
        {
            await using var host = await AlbaHost.For<global::Program>();
            await host.Scenario(_ =>
            {
                _.Get.Url("/api/Account/Register");
                _.Post.Json($$"""{ "UserName": "{{username}}",Password:{{password}},ConfirmPass:{{confirmpassword}}},email:{{email}}}""");
                _.StatusCodeShouldBe(400);
            });
        }
        
        [TestCase("user14", "user14@gmail.com", "Secret1_", "Secret1_")]
        public async Task AccountController_Register_ShoulReturn200(string username, string email, string password, string confirmPassword)
        {
            await using var host = await AlbaHost.For<global::Program>(x =>
            {
                x.UseEnvironment("Testing");
            });

            //await using var host = await AlbaHost.For<global::Program>();

            RegistrationCommand registrationCommand = new RegistrationCommand();
            registrationCommand.Password = password;
            registrationCommand.UserName = username;
            registrationCommand.Email = email;
            registrationCommand.ConfirmPass = confirmPassword;

            await host.Scenario(_ =>
            {
                _.Post.Json(registrationCommand).ToUrl("/api/Account/Register");
                _.StatusCodeShouldBe(200);
            });

            //_scope.Dispose();

        }
        //[Category("Integration")]
        //public async Task AccountController_GetPersonalData_ShouldReturn200()
        //{
        //    var securityStub = new AuthenticationStub()
        //        .With(JwtRegisteredClaimNames.Email, "user1@gmail.com")
        //        .WithName("user1");

        //    await using var host = await AlbaHost.For<global::Program>();
        //    await host.Scenario(_ =>
        //    {
        //        _.Get.Url("/Api/Account/GetPersonalData");
        //    });
        //}

        //public void Dispose()
        //{
        //    _scope.Dispose();
        //}
    }
}