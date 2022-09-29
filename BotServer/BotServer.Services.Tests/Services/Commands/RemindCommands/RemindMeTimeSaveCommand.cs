using BotServer.Services.Services.Commands;
using BotServer.Services.Services.Commands.RemindCommands;
using MediatR;
using Moq;

namespace BotServer.Services.Tests.Services.Commands.RemindCommands
{
    public class RemindMeTimeSaveCommand
    {
        [TestCase("remind", false)]
        [TestCase("2022-09-29 11:28 message", true)]
        [TestCase("remind", false)]

        public async Task IsCorrectMessage_withStatus(string message, bool status)
        {
            //arrange
            var MediatrMock = new Mock<IMediator>();
            //MediatrMock.Setups()
            var respMessage = "";
            RemindMeTaimeSaveCommand remindMeTaimeSaveCommand = new RemindMeTaimeSaveCommand(MediatrMock.Object);


            //act
            var res = remindMeTaimeSaveCommand.CanProcess(new Command(message));
            if (res)
            {
                respMessage = await remindMeTaimeSaveCommand.ProcessCommand(new Command(message));
            }

            //assert
            if (status)
            {
                Assert.IsTrue(res);
                Assert.IsTrue(!string.IsNullOrEmpty(respMessage));
            }
            else
            {
                Assert.IsFalse(res);
                Assert.IsFalse(!string.IsNullOrEmpty(respMessage));

            }
        }
    }
}
