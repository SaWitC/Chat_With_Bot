using BotServer.Application.Repositories;
using BotServer.Data.Data;
using BotServer.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace BotServer.Data.Repositories
{
    public class SelectRepository:ISelectRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public SelectRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public IEnumerable<T> SelectByTitle<T>(string Title, int page, int size) where T : class, IHasTitle, IEntity
        {
            if(!string.IsNullOrEmpty(Title))
                return _appDbContext.Set<T>().Where(x => x.Title.ToLower().Contains(Title.ToLower())).Skip(page*size).Take(size);
            return _appDbContext.Set<T>().Skip(page * size).Take(size);

        }

        public IEnumerable<T> SelectPage<T>(int page, int size) where T : class, IEntity
        {
            return _appDbContext.Set<T>().Skip(page* size).Take(size);
        }

        public IEnumerable<T> SelectByCreatedTime<T>(int page = 0, int size = 5,bool DESC=false) where T : class, IHasCreated, IEntity
        {
            if(DESC)
                return _appDbContext.Set<T>().OrderByDescending(x => x.Created).Skip(page * size).Take(size);
            return _appDbContext.Set<T>().OrderBy(x => x.Created).Skip(page * size).Take(size);
        }

        public IEnumerable<TKind> SelectWithSortByTimeByParentId<TParents, TKind>(string parentId, int page = 0, int size = 5, bool DESC = false) where TKind : class, IHasCreated,IHasParent, IEntity where TParents : class, IEntity
        {
            if (DESC)
                return _appDbContext.Set<TKind>().Where(x=>x.ParentId== parentId).OrderByDescending(x => x.Created).Skip(page * size).Take(size);
            return _appDbContext.Set<TKind>().Where(x=>x.ParentId== parentId).OrderBy(x => x.Created).Skip(page * size).Take(size);
        }

       

        public int CountPages<T>(int size) where T : class, IEntity
        {
            var count =_appDbContext.Set<T>().Count();
            return count/size;
        }

        public int CountPagesWithParent<T>(int size, string parentId) where T : class, IEntity, IHasParent
        {
            var count = _appDbContext.Set<T>().Where(o=>o.ParentId==parentId).Count();
            return count / size;
        }
    }
}
