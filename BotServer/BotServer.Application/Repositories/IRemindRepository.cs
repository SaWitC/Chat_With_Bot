using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.Repositories
{
    public interface IRemindRepository
    {
        public Task<IEnumerable<RemindModel>> GetActualAndExpiredRemindsByAvtorId(string AvtorId);
        public Task<IEnumerable<RemindModel>> GetActualRemindsByAvtorId(string AvtorId);

    }
}
