using BotServer.Application.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Services.Services.Commands
{
    public class Command : ICommand
    {
        public string CommandString 
        {
            get
            {
                return this.comandString;
            }
        }

        private string comandString;

        public Command(string commandString)
        {
            this.comandString = commandString;
        }
    }
}
