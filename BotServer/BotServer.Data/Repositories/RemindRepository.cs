using BotServer.Application.Repositories;
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
    public class RemindRepository : IRemindRepository
    {
        private readonly AppDbContext _context;
        public RemindRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RemindModel>> GetActualAndExpiredRemindsByAvtorId(string AvtorId)
        {
            return await _context.Reminds
                .Where(x => x.AvtorId == AvtorId)
                .Where(x => x.IsDeleted == false)
                .Where(x => x.RemindAtTime < DateTime.Now.AddHours(1))
                .ToListAsync();
        }

        public async Task<IEnumerable<RemindModel>> GetActualRemindsByAvtorId(string AvtorId)
        {
            return await _context.Reminds
                .Where(x => x.AvtorId == AvtorId)
                .Where(x => x.IsDeleted == false)
                .Where(x=>x.RemindAtTime<DateTime.Now.AddHours(1))
                .ToListAsync();
        }
    }
}
