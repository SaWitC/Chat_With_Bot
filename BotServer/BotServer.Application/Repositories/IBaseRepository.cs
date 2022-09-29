using BotServer.Domain.Models.Interfaces;

namespace BotServer.Application.Repositories
{
    public interface IBaseRepository
    {
        Task<T> FictiveRemove<T>(string Id) where T : class, IFictiveRemove, IEntity;
        Task SaveChangesAsync();
        Task<T> Create<T>(T model) where T : class, IEntity;
        Task<T> Update<T>(T model, string Id) where T : class, IEntity;


        Task<bool> Delete<T>(string Id) where T : class, IEntity;

        Task<IEnumerable<T>> GetAll<T>() where T : class, IEntity;

        Task<T> GetByid<T>(string id) where T : class, IEntity;

        Task<TKind> Create<TParent, TKind>(string ParentId, TKind model) where TKind : class, IEntity, IHasParent where TParent : class, IEntity;
    }
}
