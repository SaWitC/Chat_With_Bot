using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Services.Services.AditionalServices.Vk
{
    public interface IvkApiService
    {
        public Task<long?> GetUserId(string Password, string Email);
    }
}
