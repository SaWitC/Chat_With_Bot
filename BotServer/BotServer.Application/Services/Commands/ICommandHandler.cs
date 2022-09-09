using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.Services.Commands
{
    public interface ICommandHandler
    {
        Task<string> ProcessCommand(ICommand command);
        bool CanProcess(ICommand command);
    }
}
