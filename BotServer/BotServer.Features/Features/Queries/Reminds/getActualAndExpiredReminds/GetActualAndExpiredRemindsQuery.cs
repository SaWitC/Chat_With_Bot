using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Reminds.getActualAndExpiredReminds
{
    public class GetActualAndExpiredRemindsQuery:IRequest<IEnumerable<RemindModel>>
    {
        public string AvtorId { get; set; }
    }
}
