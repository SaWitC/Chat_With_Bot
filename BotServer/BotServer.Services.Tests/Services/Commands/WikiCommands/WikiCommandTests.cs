using AutoFixture;
using BotServer.Application.CustomHTTPClients;
using BotServer.Services.CustomHTTPClients.Wiki;
using BotServer.Services.Services.Commands;
using Castle.Core.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotServer.Domain.HttResponseModels.WikiModels;

namespace BotServer.Services.Tests.Services.Commands.WikiCommands
{
    public class WikiCommandTests
    {
        [TestCase("wiki Витебск", true)]
        [TestCase("wikidepia", true)]
        [TestCase("wikipedia       asdsa", true)]
        [TestCase("hello", false)]
        public async Task Must_Return_WikiMessage__WithStatus(string message, bool isCorrect)
        {
            //arrange
            Fixture fixture = new Fixture();
            var httpClientMock = new Mock<IWikiHttpClient>();
            var wikiResp = fixture.Build<Rootobject>().With(x => x.query, null).Create();

            httpClientMock.Setup(x => x.GetLinks(message)).Returns(async () => wikiResp);

            Command command = new Command(message);
            BotServer.Services.Services.Commands.WikiCommand.GetArticleLinks wikiCommand = new(httpClientMock.Object);
            var respMessage = "";
            bool canProcess = wikiCommand.CanProcess(command);

            //act

            if (canProcess)
            {
                respMessage = await wikiCommand.ProcessCommand(command);
            }
            //assert
            if (isCorrect)
            {
                Assert.IsTrue(canProcess);
                Assert.AreEqual("i can not found nothin", respMessage);
            }
            else
            {
                Assert.IsFalse(canProcess);
            }
        }
    }
}
