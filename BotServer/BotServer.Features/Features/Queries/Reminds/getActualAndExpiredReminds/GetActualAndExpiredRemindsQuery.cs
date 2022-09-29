using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Queries.Reminds.getActualAndExpiredReminds
{
    public class GetActualAndExpiredRemindsQuery : IRequest<IEnumerable<RemindModel>>
    {
        public string AvtorId { get; set; }
    }
}
