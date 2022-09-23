using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.Repositories
{
    public interface IHubRepository
    {
        Task<HubConnections> GetByHubConnection(string HubConnection);
        Task<HubConnections> CloseConnectionById(string id);

        Task<IEnumerable<HubConnections>> GetAllActivConnectionsByUser(string userId);

    }
}
