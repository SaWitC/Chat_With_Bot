using BotServer.Application.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Services.Services.Commands.RemindCommands
{
    public class RemindMeCommand : ICommandHandler
    {
        //private readonly string[] commands = new string[] { "remind" };
        public bool CanProcess(ICommand command)
        {
            if(command.CommandString.ToLower().Contains("remind"))
                return true;
            return false;
        }

        public async Task<string> ProcessCommand(ICommand command)
        {
            return "Select date for remind";
        }
    }
}
