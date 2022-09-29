using BotServer.Application.Services.Commands;
using BotServer.Data.Attributes;

namespace BotServer.Services.Services.Commands.HelloCommands
{
    [Service]
    public class HelloCommand : ICommandHandler
    {
        public bool CanProcess(ICommand command)
        {
            return command.CommandString.ToLower().Contains("hello");
        }

        public async Task<string> ProcessCommand(ICommand command)
        {
            return "hello people";
        }
    }
}
