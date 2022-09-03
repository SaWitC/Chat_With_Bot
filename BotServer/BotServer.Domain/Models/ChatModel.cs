using BotServer.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Domain.Models
{
    public class ChatModel : IEntity,IHasTitle, IHasCreated
    {
        public string id { get; set; }

        public string avtorId { get; set; }

        public string Title { get; set; }
        public DateTime Created { get; set; }

        public IEnumerable<MessageModel> Messages { get; set; }


        //public string botId { get; set; }
        
    }
}
