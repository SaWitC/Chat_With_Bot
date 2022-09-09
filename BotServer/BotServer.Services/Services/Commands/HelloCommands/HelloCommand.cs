using BotServer.Application.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Services.Services.Commands.HelloCommands
{
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
