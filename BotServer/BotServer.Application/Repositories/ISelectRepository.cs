using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.Repositories
{
    public interface ISelectRepository
    {
        public IEnumerable<T> SelectPage<T>(int page = 0, int size = 5);
    }
}
