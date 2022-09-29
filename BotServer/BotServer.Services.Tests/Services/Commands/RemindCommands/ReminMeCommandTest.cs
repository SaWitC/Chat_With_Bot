using BotServer.Services.Services.Commands;
using BotServer.Services.Services.Commands.RemindCommands;

namespace BotServer.Services.Tests.Services.Commands.RemindCommands
{
    public class ReminMeCommandTest
    {
        [TestCase("remind", true)]
        [TestCase("hello", false)]
        [TestCase("help mi set remind", true)]

        public async Task IsCorrectMessage(string message, bool status)
        {
            RemindMeCommand remindMeCommand = new RemindMeCommand();


            var res = remindMeCommand.CanProcess(new Command(message));

            string resMessage = "";
            if (status)
            {
                resMessage = await remindMeCommand.ProcessCommand(new Command(message));
                Assert.IsTrue(res);
                Assert.IsTrue(!string.IsNullOrEmpty(resMessage));
                Assert.AreEqual(resMessage, "Select date for remind");

            }
            else
            {
                Assert.IsTrue(string.IsNullOrEmpty(resMessage));
                Assert.IsFalse(res);
            }

        }
    }
}
