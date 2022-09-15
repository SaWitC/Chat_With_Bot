using BotServer.Domain.Models.Short;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
