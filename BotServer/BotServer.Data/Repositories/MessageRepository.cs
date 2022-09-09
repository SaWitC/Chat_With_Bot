using AutoMapper;
using AutoMapper.QueryableExtensions;
using BotServer.Application.Repositories;
using BotServer.Data.Data;
using BotServer.Domain.Models.Short;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public MessageRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public IEnumerable<MessageSortModel> SelectWithSortByTimeByParentId(string parentId, int page = 0, int size = 5, bool DESC = false)
        {
            if (DESC)
                return _appDbContext.Messages.Where(x => x.ParentId == parentId).OrderByDescending(x => x.Created).Skip(page * size).ProjectTo<MessageSortModel>(_mapper.ConfigurationProvider).Take(size);
            return _appDbContext.Messages.Where(x => x.ParentId == parentId).OrderBy(x => x.Created).Skip(page * size).ProjectTo<MessageSortModel>(_mapper.ConfigurationProvider).Take(size);
        }


    }
}
