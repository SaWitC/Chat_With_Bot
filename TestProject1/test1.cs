using BotServer.Features.Features.Account.RegistrationCommand;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TestProject1
{
    public class test1
    {

        [Fact]
        public void TestMethod1()
        {
            RegistrationCommand registrationCommand = new RegistrationCommand();
            registrationCommand.Password = "Secret1_";
            registrationCommand.UserName = "User21";
            registrationCommand.Email = "User21@gmail.com";
            registrationCommand.ConfirmPass = "Secret1_";

            var json = JsonSerializer.Serialize(registrationCommand);

            HttpContent content = JsonContent.Create(json);

            //var response = await _client.PostAsync("/api/Account/Register",content);

            ////Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);

            //Assert.AreEqual(HttpStatusCode.OK,response.StatusCode);

            var webAppFactory = new WebApplicationFactory<Program>();

            var httpClient = webAppFactory.CreateDefaultClient();

            var response = httpClient.GetAsync("https://localhost:7126/api/Vk/TestMethod/1");

            //var response = httpClient.PostAsync("/api/Account/Register",content);

            Assert.Equal(response.Result.StatusCode, HttpStatusCode.OK);
        }
    }
}
