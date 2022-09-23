using BotServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.BackgroundJobs.Remind
{
    public interface IBGJobRemind
    {
        public Task<bool> SetBgJobRemind(RemindModel remind);
    }
}
