using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Queries.Reminds.getActualAndExpiredReminds
{
    public class GetActualAndExpiredRemindsQueryHandler : IRequestHandler<GetActualAndExpiredRemindsQuery, IEnumerable<RemindModel>>
    {
        private readonly IRemindRepository _remindRepository;

        public GetActualAndExpiredRemindsQueryHandler(IRemindRepository remindRepository)
        {
            _remindRepository = remindRepository;
        }

        public async Task<IEnumerable<RemindModel>> Handle(GetActualAndExpiredRemindsQuery request, CancellationToken cancellationToken)
        {
            return await _remindRepository.GetActualAndExpiredRemindsByAvtorId(request.AvtorId);
        }
    }
}
