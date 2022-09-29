namespace BotServer.Application.Repositories
{
    public interface IHubRepository
    {
        Task<HubConnections> GetByHubConnection(string HubConnection);
        Task<HubConnections> CloseConnectionById(string id);

        Task<IEnumerable<HubConnections>> GetAllActivConnectionsByUser(string userId);

    }
}
