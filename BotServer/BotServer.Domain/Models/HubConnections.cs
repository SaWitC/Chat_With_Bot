using BotServer.Domain.Models.Interfaces;

namespace BotServer.Domain.Models
{
    public class HubConnections : IEntity
    {
        public string id { get; set; }
        public string AvtorId { get; set; }
        public string HubConnection { get; set; }
        public bool IsClosed { get; set; } = false;
    }
}
