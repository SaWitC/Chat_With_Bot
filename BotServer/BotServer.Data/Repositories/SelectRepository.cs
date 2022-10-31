using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Data.Attributes;
using BotServer.Data.Data;
using BotServer.Data.Specifications;
using BotServer.Domain.Models;
using BotServer.Domain.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BotServer.Data.Repositories
{
    [Service]
    public class SelectRepository : ISelectRepository
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
            var specification = new GetPageSpecification<T>(page, size);

            if (!string.IsNullOrEmpty(Title))
                return SpecificationEvaluator.Default.GetQuery(_appDbContext.Set<T>().AsNoTracking().Where(x => x.Title.ToLower().Contains(Title.ToLower())), specification);
            return SpecificationEvaluator.Default.GetQuery(_appDbContext.Set<T>().AsNoTracking(), specification);

        }

        public IEnumerable<T> SelectPage<T>(int page, int size) where T : class, IEntity=>   
            _appDbContext.Set<T>().AsNoTracking().Skip(page * size).Take(size);
        

        public IEnumerable<T> SelectByCreatedTime<T>(int page = 0, int size = 5, bool DESC = false) where T : class, IHasCreated, IEntity
        {
            var specification = new GetPageSpecification<T>(page, size);

            if (DESC)
                return SpecificationEvaluator.Default.GetQuery(_appDbContext.Set<T>().AsNoTracking().OrderByDescending(x => x.Created), specification);
            return SpecificationEvaluator.Default.GetQuery(_appDbContext.Set<T>().AsNoTracking().OrderBy(x => x.Created), specification);
        }

        public IEnumerable<TKind> SelectWithSortByTimeByParentId<TParents, TKind>(string parentId, int page = 0, int size = 5, bool DESC = false) where TKind : class, IHasCreated, IHasParent, IEntity where TParents : class, IEntity
        {
            var specification = new GetPageSpecification<TKind>(page, size);

            if (DESC)
                return SpecificationEvaluator.Default.GetQuery(_appDbContext.Set<TKind>().AsNoTracking().Where(x => x.ParentId == parentId).OrderByDescending(x => x.Created),specification);
            return SpecificationEvaluator.Default.GetQuery(_appDbContext.Set<TKind>().AsNoTracking().Where(x => x.ParentId == parentId).OrderBy(x => x.Created), specification);

        }

        public int CountPages<T>(int size) where T : class, IEntity=>
           _appDbContext.Set<T>().AsNoTracking().Count()/size;
            
        public int CountPagesWithParent<T>(int size, string parentId) where T : class, IEntity, IHasParent=>
            _appDbContext.Set<T>().AsNoTracking().Where(o => o.ParentId == parentId).Count()/size;
        
    }
}
