using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using BotServer.Application.Repositories;
using BotServer.Data.Attributes;
using BotServer.Data.Data;
using BotServer.Data.Specifications;
using BotServer.Domain.Models;

namespace BotServer.Data.Repositories
{
    [Service]
    public class ChatRepository : RepositoryBase<ChatModel>, IChatRepository
    {
        private readonly AppDbContext _appDbContext;

        public ChatRepository(AppDbContext appDbContext):base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //public IEnumerable<ChatModel> GetPageByAvtorId(string avtorId, int page = 0, int size = 5)
        //{
        //    if (!string.IsNullOrEmpty(avtorId))
        //        return _appDbContext.Chats.Where(x => x.avtorId == avtorId).Skip(page * size).Take(size);
        //    else
        //        throw new ArgumentException("avtorId can not be null or empty");
        //}

        public IEnumerable<ChatModel> GetPageByAvtorId(string avtorId, int page = 0, int size = 5)
        {
            var specification = new GetPageSpecification<ChatModel>(page,size);
            if (!string.IsNullOrEmpty(avtorId))
                return SpecificationEvaluator.Default.GetQuery(_appDbContext.Chats.Where(x => x.avtorId == avtorId), specification);
            //return _appDbContext.Chats.Where(x => x.avtorId == avtorId).Skip(page * size).Take(size);
            else
                throw new ArgumentException("avtorId can not be null or empty");
        }

    }
}
