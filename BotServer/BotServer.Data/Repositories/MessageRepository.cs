using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BotServer.Application.Repositories;
using BotServer.Data.Attributes;
using BotServer.Data.Data;
using BotServer.Data.Specifications;
using BotServer.Domain.Models;
using BotServer.Domain.Models.Short;
using Microsoft.EntityFrameworkCore;

namespace BotServer.Data.Repositories
{
    [Service]
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public MessageRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public IEnumerable<MessageShortModel> SelectWithSortByTimeByParentId(string parentId, int page = 0, int size = 5, bool DESC = false)
        {
            if (DESC)
                return _appDbContext.Messages.AsNoTracking().Where(x => x.ParentId == parentId).OrderByDescending(x => x.Created).Skip(page * size).ProjectTo<MessageShortModel>(_mapper.ConfigurationProvider).Take(size);
            return _appDbContext.Messages.AsNoTracking().Where(x => x.ParentId == parentId).OrderBy(x => x.Created).Skip(page * size).ProjectTo<MessageShortModel>(_mapper.ConfigurationProvider).Take(size);
        }


    }
}
