using BotServer.Domain.Models.Short;

namespace BotServer.Domain.Models.Details
{
    public class ChatDetailsModel
    {
        public string id { get; set; }

        public string avtorId { get; set; }

        public string Title { get; set; }
        public DateTime Created { get; set; }

        public IEnumerable<MessageShortModel> Messages { get; set; }

        public int Page { get; set; }
    }
}
