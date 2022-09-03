using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Chat.GetChatById
{
    public class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, ChatModel>
    {
        public IBaseRepository _baseRepository;
        public ISelectRepository _selectRepository;
        public GetChatByIdQueryHandler(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ChatModel> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
        {
            var chat =await _baseRepository.GetByid<ChatModel>(request.id);
            if (chat != null)
            {
                chat.Messages = _selectRepository.SelectWithSortByTimeByParentId<ChatModel,MessageModel>(chat.id,DESC:true);
            }
            return chat;
        }
    }
}
