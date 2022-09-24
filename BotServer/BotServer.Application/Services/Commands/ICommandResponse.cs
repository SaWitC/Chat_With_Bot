using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.Services.Commands
{
    public interface ICommandResponse
    {
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
