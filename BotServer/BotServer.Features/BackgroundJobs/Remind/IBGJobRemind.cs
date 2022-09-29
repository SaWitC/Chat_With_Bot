using BotServer.Domain.Models;

namespace BotServer.Features.BackgroundJobs.Remind
{
    public interface IBGJobRemind
    {
        public Task<bool> SetBgJobRemind(RemindModel remind);
    }
}
