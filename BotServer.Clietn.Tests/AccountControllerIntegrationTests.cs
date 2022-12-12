using Alba.Security;
using Microsoft.IdentityModel.JsonWebTokens;
using NUnit.Framework;

namespace BotServer.Clietn.Tests
{
    public class AccountControllerIntegrationTests
    {
        [Fact]
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
        public async Task AccountController_Login_ShoulReturn200()
        {
            await using var host = await AlbaHost.For<global::Program>();

            await host.Scenario(_ =>
            {
                _.Get.Url("/api/Account/Login");
                _.Post.Json("""{ "userName": "user1","password": "Secret1_"}""");
                _.StatusCodeShouldBe(200);
            });
        }
        //[TestCase("user1","user1@gmail.com","Secret1_","Secret1_")]
        [TestCase("1", "1", "1", "1")]
        [TestCase("user1", "user1@gmail.com", "Secret2_", "Secret1_")]
        [TestCase("user1", "user1@.com", "Secret1_", "Secret1_")]
        [TestCase("user1", "user1@gmail.com", "Secret1", "Secret1")]
        [TestCase("user1", "user1@gmail.com", "Secret_", "Secret_")]


        public async Task AccountController_Register_ShoulReturn400(string username,string email, string password,string confirmpassword)
        {
            await using var host = await AlbaHost.For<global::Program>();
            await host.Scenario(_ =>
            {
                _.Get.Url("/api/Account/Register");
                _.Post.Json($$"""{ "userName": "{{username}}",password:{{password}},confirmpassword:{{confirmpassword}}},email:{{email}}""");
                _.StatusCodeShouldBe(400);
            });
        }

        public async Task AccountController_GetPersonalData_ShouldReturn200()
        {
            var securityStub = new AuthenticationStub()
                .With(JwtRegisteredClaimNames.Email, "user1@gmail.com")
                .WithName("user1");

            await using var host = await AlbaHost.For<global::Program>();
            await host.Scenario(_ =>
            {
                _.Get.Url("/Api/Account/GetPersonalData");
            });
        }

    }
}