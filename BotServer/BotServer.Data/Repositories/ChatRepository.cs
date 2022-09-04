using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotServer.Application.Repositories;
using BotServer.Data.Data;
using BotServer.Domain.Models;

namespace BotServer.Data.Repositories
{
    public class ChatRepository: IChatRepository
    {
        private readonly AppDbContext _appDbContext;

        public ChatRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<ChatModel> GetPageByAvtorId(string avtorId, int page = 0, int size = 5)
        {
            if (!string.IsNullOrEmpty(avtorId))
                return _appDbContext.Chats.Where(x => x.avtorId == avtorId).Skip(page * size).Take(size);
            else
                throw new ArgumentException("avtorId can not be null or empty");
        }

    }
}
