using Alba;
using Alba.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BotServer.ClientPart.Tests.Fixtures;
using BotServer.Features.Features.Account.RegistrationCommand;
using BotServer.Features.Features.Commands.Chat.CreateChatCommand;
using MassTransit.Internals;

namespace BotServer.ClientPart.Tests
{
    [TestFixture]
    public class ChatController
    {

        private BaseFixture fixture = new();

        [OneTimeSetUp]
        public async Task Initialize()
        {
            await fixture.Init();
        }

        [OneTimeTearDown]
        public async Task Dispose()
        {
            fixture.Dispose();
            //await BaseFixture
        }
        [Test]
        [Category("Integration")]
        public async Task ChatController_GetMyChats_ShouldReturn401()
        {
            var securityStub = new AuthenticationStub()
                .With(JwtRegisteredClaimNames.Email, "user1@gmail.com")
                .With(JwtRegisteredClaimNames.NameId, Guid.Empty.ToString());

            fixture.Host.Scenario(o =>
            {
                o.Get.Url("/api/Chat/getMy/1");
                o.StatusCodeShouldBe(401);
            });
        }

        [Test]
        [Category("Integration")]
        public async Task ChatController_GetMyChats_ShouldReturn200()
        {
            var securityStub = new AuthenticationStub()
                .With(JwtRegisteredClaimNames.Email, "user1@gmail.com")
                .With(JwtRegisteredClaimNames.NameId, Guid.Empty.ToString());

            await fixture.RegisterAndLoginTestAccount_user1();
            var token = "Bearer " + fixture.JwtToken;
            await fixture.Host.Scenario(o =>
            {
                o.WithRequestHeader("Authorization", @token.Replace("\"", ""));
                o.Get.Url("/api/Chat/getMy/1");
                o.StatusCodeShouldBe(200);
            });
        }

       
        //[Test]
            //public async Task ChatController_GetMyChats_ShouldReturn200()
            //{
            //    var securityStub = new AuthenticationStub()
            //        .With(JwtRegisteredClaimNames.Email, "user1@gmail.com")
            //        .With(JwtRegisteredClaimNames.NameId, Guid.Empty.ToString());

            //    var theHost = await AlbaHost.For<Program>(securityStub);

            //    var resp = theHost.Scenario(o =>
            //    {
            //        o.Get.Url("https://localhost:7126/api/Chat/getMy/1");
            //        o.StatusCodeShouldBe(200);
            //    });
            //}
        }
}
