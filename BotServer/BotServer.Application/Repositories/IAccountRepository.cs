namespace BotServer.Application.Repositories
{
    public interface IAccountRepository
    {
        Task<string> GenerateJwtToken(User user);
    }
}
