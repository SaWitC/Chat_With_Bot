using BotServer.Application.Repositories;
using BotServer.Data.Attributes;
using BotServer.Data.Data;
using BotServer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BotServer.Data.Repositories
{
    [Service]
    public class RemindRepository : IRemindRepository
    {
        private readonly AppDbContext _context;
        public RemindRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RemindModel>> GetActualAndExpiredRemindsByAvtorId(string AvtorId)
        {
            if (!string.IsNullOrEmpty(AvtorId))
            {
                return await _context.Reminds
                .Where(x => x.AvtorId == AvtorId)
                .Where(x => x.IsDeleted == false)
                .Where(x => x.RemindAtTime < DateTime.Now.AddHours(1))
                .ToListAsync();
            }
            else
            {
                throw new ArgumentException("AvtorId can not be null");
            }
        }

        public async Task<IEnumerable<RemindModel>> GetActualRemindsByAvtorId(string AvtorId)
        {
            if (!string.IsNullOrEmpty(AvtorId))
            {
                return await _context.Reminds
                .Where(x => x.AvtorId == AvtorId)
                .Where(x => x.IsDeleted == false)
                .Where(x => x.RemindAtTime < DateTime.Now.AddHours(1))
                .ToListAsync();
            }
            else
            {
                throw new ArgumentException("AvtorId can not be null");
            }
        }

        public async Task<IEnumerable<RemindModel>> GetExpiredRemindsbyAvorId(string AvtorId)
        {
            if (!string.IsNullOrEmpty(AvtorId))
            {
                return await _context.Reminds
                   .Where(x => x.AvtorId == AvtorId)
                   .Where(x => x.IsDeleted == false)
                   .Where(x => x.RemindAtTime < DateTime.Now)
                   .ToListAsync();
            }
            else
            {
                throw new ArgumentException("AvtorId can not be null");
            }
        }
    }
}
