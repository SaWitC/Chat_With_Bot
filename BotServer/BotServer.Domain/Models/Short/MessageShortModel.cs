using BotServer.Domain.Models.Interfaces;

namespace BotServer.Domain.Models.Short
{
    public class MessageShortModel : IEntity
    {
        public string id { get; set; }
        public string text { get; set; }
        public string avtroId { get; set; }
        public bool IsFromBot { get; set; } = false;

        public DateTime Created { get; set; }
    }
}
