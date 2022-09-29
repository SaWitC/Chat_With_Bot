using AutoFixture;
using BotServer.Application.CustomHTTPClients;
using BotServer.Services.Services.Commands;
using Microsoft.Extensions.Configuration;
using Moq;
using static BotServer.Domain.HttResponseModels.WeatherModels;

namespace BotServer.Services.Tests.Services.Commands.WeatherCommands
{
    public class WeatherCommandTest
    {
        [TestCase("weather Витебск", true, true)]
        [TestCase("weather", true, false)]
        [TestCase("weather       asdsa", true, false)]
        [TestCase("hello", false, false)]

        public async Task Must_Return_WeatherMessage__withStatus(string message, bool isCorrect, bool IsCorrectLocation)
        {
            //arrange
            Fixture fixture = new Fixture();
            var httpClientMock = new Mock<IWeatherHttpClient>();
            var weatherResp = fixture.Build<Rootobject>().Create();

            httpClientMock.Setup(x => x.GetWeather(message)).Returns(async () => weatherResp);

            var iConfigurationMock = new Mock<IConfiguration>();
            iConfigurationMock.Setup(x => x["OpenWeatherMap:Token"]).Returns("token");

            Command command = new Command(message);
            BotServer.Services.Services.Commands.WeatherCommands.GetCurrentWeatherCommand weatherCommand = new(httpClientMock.Object, iConfigurationMock.Object);
            var respMessage = "";
            bool canProcess = weatherCommand.CanProcess(command);

            //act

            if (canProcess)
            {
                respMessage = await weatherCommand.ProcessCommand(command);
            }
            //assert
            if (isCorrect && IsCorrectLocation)
            {
                Assert.IsTrue(canProcess);
                Assert.AreEqual("Sory but I can't found any weather", respMessage);
            }
            else if (canProcess && !IsCorrectLocation)
            {
                Assert.IsTrue(canProcess);
                Assert.AreEqual("Write location for example 'weather in moscow'", respMessage);
            }
            else
            {
                Assert.IsFalse(canProcess);
            }
        }
    }
}
