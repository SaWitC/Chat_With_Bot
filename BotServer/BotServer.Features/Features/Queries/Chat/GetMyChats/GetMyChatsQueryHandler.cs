using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Queries.Chat.GetMyChats
{
    public class GetMyChatsQueryHandler : IRequestHandler<GetMyChatsQuery, IEnumerable<ChatModel>>
    {

        public IChatRepository _chatRepository;
        public GetMyChatsQueryHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<IEnumerable<ChatModel>> Handle(GetMyChatsQuery request, CancellationToken cancellationToken)=>
            _chatRepository.GetPageByAvtorId(request.AvtorId, request.Page);
        
    }
}
