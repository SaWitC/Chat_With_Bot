using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Queries.Reminds.getActualAndExpiredReminds
{
    public class GetActualAndExpiredRemindsQueryHandler : IRequestHandler<GetActualAndExpiredRemindsQuery, IEnumerable<RemindModel>>
    {
        private readonly IRemindRepository _remindRepository;
        //private readonly IHttpContextAccessor _accessor;

        public GetActualAndExpiredRemindsQueryHandler(IRemindRepository remindRepository/*, IHttpContextAccessor accessor*/)
        {
            _remindRepository = remindRepository;
            //_accessor = accessor;
        }

        public async Task<IEnumerable<RemindModel>> Handle(GetActualAndExpiredRemindsQuery request, CancellationToken cancellationToken)
        {

            var reminds = await _remindRepository.GetActualAndExpiredRemindsByAvtorId(request.AvtorId);

            //if (reminds != null) {
            return reminds;

        }
    }
}
