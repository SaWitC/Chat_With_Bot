namespace BotServer.Services.Services.AditionalServices.Vk
{
    public interface IvkApiService
    {
        public Task<long?> GetUserId(string Password, string Email);
    }
}
