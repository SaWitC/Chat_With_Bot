using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Domain.Models.Interfaces
{
    public class Bot : IEntity
    {
        public string id { get; set; }
    }
}
