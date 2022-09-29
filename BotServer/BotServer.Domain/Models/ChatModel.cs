using BotServer.Domain.Models.Interfaces;

namespace BotServer.Domain.Models
{
    public class ChatModel : IEntity, IHasTitle, IHasCreated
    {
        public string id { get; set; }

        public string avtorId { get; set; }

        public string Title { get; set; }
        public DateTime Created { get; set; }

        public IEnumerable<MessageModel> Messages { get; set; }


        //public string botId { get; set; }

    }
}
