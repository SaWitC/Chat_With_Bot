using BotServer.Application.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Services.Services.Commands.WeatherCommands
{
    public class GetCurrentWeatherCommand : ICommandHandler
    {
        public bool CanProcess(ICommand command)
        {
            return command.CommandString.ToLower().Contains("weather");
        }

        public async Task<string> ProcessCommand(ICommand command)
        {
            return "XD I do not know";      
        }
    }
}
