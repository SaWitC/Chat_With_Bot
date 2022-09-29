namespace BotServer.Application.Repositories
{
    public interface IChatRepository
    {

        //public Task<EntityEntry<ChatModel> Create(Cha model) where T : class, IEntity;

        //public Task<bool> Remowe<T>(string id) where T : class, IEntity;

        //public Task<EntityEntry<T>> Update<T>(T model, string id) where T : class, IEntity;

        //public Task<T> GetById<T>(string id) where T : class, IEntity;

        public IEnumerable<ChatModel> GetPageByAvtorId(string avtorId, int page = 0, int size = 5);
    }
}
