using BotServer.Domain.Models.Short;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.Repositories
{
    public interface IMessageRepository
    {
        public IEnumerable<MessageShortModel> SelectWithSortByTimeByParentId(string parentId, int page = 0, int size = 5, bool DESC = false);
    }
}
