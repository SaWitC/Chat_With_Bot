using BotServer.Domain.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotServer.Domain.Models
{
    public class MessageModel : IEntity, IHasCreated, IHasParent
    {
        public string id { get; set; }
        public string text { get; set; }
        public string avtroId { get; set; }
        public string ParentId { get; set; }
        [ForeignKey("ParentId")]
        public ChatModel chat { get; set; }
        public bool IsFromBot { get; set; } = false;

        public DateTime Created { get; set; }

    }
}
