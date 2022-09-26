using BotServer.Application.Repositories;
using BotServer.Data.Attributes;
using BotServer.Data.Data;
using BotServer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Data.Repositories
{
    [Service]
    public class HubRepository : IHubRepository
    {
        private readonly AppDbContext _context;
        public HubRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HubConnections> CloseConnectionById(string id)
        {
            var connection =await _context.HubConnections.FirstOrDefaultAsync(x => x.id == id);
            if (connection != null)
            {
                connection.IsClosed = true;
                return connection;
            }
            return null;
        }

        public async Task<IEnumerable<HubConnections>> GetAllActivConnectionsByUser(string userId)
        {
            return await _context.HubConnections.Where(x => x.AvtorId == userId).Where(x=>x.IsClosed==false).ToListAsync();
        }

        public async Task<HubConnections> GetByHubConnection(string HubConnection)
        {
            return await _context.HubConnections.FirstOrDefaultAsync(x=>x.HubConnection==HubConnection&&x.IsClosed==false);
        }
    }
}
