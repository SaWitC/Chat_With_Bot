using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.Repositories
{
    public interface IAccountRepository
    {
        Task<string> GenerateJwtToken(User user);
    }
}
