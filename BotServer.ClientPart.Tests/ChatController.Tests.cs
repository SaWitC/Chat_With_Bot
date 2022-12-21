using Alba;
using Alba.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.ClientPart.Tests
{
    public class ChatController
    {
        [Test]
        public async Task ChatController_GetMyChats_ShouldReturn401()
        {
            var securityStub = new AuthenticationStub()
                .With(JwtRegisteredClaimNames.Email, "user1@gmail.com")
                .With(JwtRegisteredClaimNames.NameId, Guid.Empty.ToString());

            var theHost = await AlbaHost.For<Program>(securityStub);

            var resp = theHost.Scenario(o =>
            {
                o.Get.Url("https://localhost:7126/api/Chat/getMy/1");
                o.StatusCodeShouldBe(401);
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
