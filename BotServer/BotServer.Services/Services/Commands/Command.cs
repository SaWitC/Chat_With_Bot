using BotServer.Application.Services.Commands;

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
