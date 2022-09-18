using BotServer.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Domain.Models
{
    public class RemindModel:IEntity,IFictiveRemove
    {
        public string id { get; set; }
        [ForeignKey("ParentId")]
        public string RemindMessage { get; set; }
        public DateTime Created { get; set; }
        public DateTime RemindAtTime { get; set; }
        
        public string AvtorId { get; set; }

        public bool IsDeleted { get; set; }

    }
}
