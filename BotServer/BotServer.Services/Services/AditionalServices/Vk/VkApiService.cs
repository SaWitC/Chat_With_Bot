using VkNet.Abstractions;
using VkNet.Model;

namespace BotServer.Services.Services.AditionalServices.Vk
{
    public class VkApiService : IvkApiService
    {
        private readonly IVkApi _vkApi;
        public VkApiService(IVkApi vkApi)
        {
            _vkApi = vkApi;
        }
        public async Task<long?> GetUserId(string Password, string Email)
        {
            await _vkApi.AuthorizeAsync(new ApiAuthParams() { Login = Email, Password = Password });

            return _vkApi.UserId;
        }
    }
}
