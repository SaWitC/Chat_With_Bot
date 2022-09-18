using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Domain.Models.Interfaces
{
    public interface IFictiveRemove
    {
        public bool IsDeleted { get; set; }
    }
}
