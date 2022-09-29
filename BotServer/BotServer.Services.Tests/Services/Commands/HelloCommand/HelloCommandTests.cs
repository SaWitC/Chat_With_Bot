using BotServer.Services.Services.Commands;

namespace BotServer.Services.Tests.Services.Commands.HelloCommand
{
    public class HelloCommandTests
    {
        [TestCase("hello", true)]
        [TestCase("HeLlO", true)]
        [TestCase("bye", false)]
        public async Task MustReturn_message_hello_people(string message, bool isCorrect)
        {
            //arrange
            Command command = new Command(message);
            BotServer.Services.Services.Commands.HelloCommands.HelloCommand helloCommand = new();
            var respMessage = "";

            //act
            var canProcess = helloCommand.CanProcess(command);
            if (canProcess)
            {
                respMessage = await helloCommand.ProcessCommand(command);
            }
            //assert
            if (isCorrect)
            {
                Assert.IsTrue(canProcess);
                Assert.AreEqual("hello people", respMessage);
            }
            else
            {
                Assert.IsFalse(canProcess);
            }

        }
    }
}
