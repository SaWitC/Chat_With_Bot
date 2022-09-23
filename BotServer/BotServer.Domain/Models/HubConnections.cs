using BotServer.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Domain.Models
{
    public class HubConnections:IEntity
    {
        public string id { get; set; }
        public string AvtorId { get; set; }
        public string HubConnection { get; set; }
        public bool IsClosed { get; set; } = false;
    }
}
