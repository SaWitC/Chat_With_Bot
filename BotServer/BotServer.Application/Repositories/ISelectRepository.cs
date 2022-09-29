using BotServer.Domain.Models.Interfaces;

namespace BotServer.Application.Repositories
{
    public interface ISelectRepository
    {
        public IEnumerable<T> SelectPage<T>(int page = 0, int size = 5) where T : class, IEntity;

        public IEnumerable<T> SelectByTitle<T>(string Title, int page = 0, int size = 5) where T : class, IHasTitle, IEntity;
        public IEnumerable<T> SelectByCreatedTime<T>(int page = 0, int size = 5, bool DESC = false) where T : class, IHasCreated, IEntity;

        public IEnumerable<TKind> SelectWithSortByTimeByParentId<TParents, TKind>(string parentsId, int page = 0, int size = 5, bool DESC = false) where TKind : class, IHasCreated, IHasParent, IEntity where TParents : class, IEntity;



        public int CountPages<T>(int size) where T : class, IEntity;
        public int CountPagesWithParent<T>(int size, string parentId) where T : class, IEntity, IHasParent;


    }
}
