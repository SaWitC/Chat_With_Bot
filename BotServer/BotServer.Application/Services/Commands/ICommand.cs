using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.Services.Commands
{
    public interface ICommand
    {
        public string CommandString { get; }
    }
}
