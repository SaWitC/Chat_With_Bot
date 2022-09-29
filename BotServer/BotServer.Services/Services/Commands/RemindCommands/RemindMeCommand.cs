using BotServer.Application.Services.Commands;
using BotServer.Data.Attributes;

namespace BotServer.Services.Services.Commands.RemindCommands
{
    [Service]
    public class RemindMeCommand : ICommandHandler
    {
        //private readonly string[] commands = new string[] { "remind" };
        public bool CanProcess(ICommand command)
        {
            if (command.CommandString.ToLower().Contains("remind"))
                return true;
            return false;
        }

        public async Task<string> ProcessCommand(ICommand command)
        {
            return "Select date for remind";
        }
    }
}
