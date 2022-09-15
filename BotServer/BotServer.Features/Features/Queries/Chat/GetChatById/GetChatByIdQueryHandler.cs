using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using BotServer.Domain.Models.Details;
using BotServer.Domain.Models.Short;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Chat.GetChatById
{
    public class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, ChatDetailsModel>
    {
        private readonly IBaseRepository _baseRepository;
        private readonly ISelectRepository _selectRepository;
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;
        public GetChatByIdQueryHandler(IBaseRepository baseRepository, ISelectRepository selectRepository,IMapper mapper, IMessageRepository messageRepository)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _selectRepository = selectRepository;
            _messageRepository = messageRepository;
        }

        public async Task<ChatDetailsModel> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
        {
            var chat =await _baseRepository.GetByid<ChatModel>(request.id);
            ChatDetailsModel ChatDetails= null;// =new ChatDetailsModel();
            if (chat != null) { 
                ChatDetails = _mapper.Map<ChatDetailsModel>(chat);
                ChatDetails.Page = _selectRepository.CountPagesWithParent<MessageModel>(5,chat.id);
                
                var messages = _messageRepository.SelectWithSortByTimeByParentId(chat.id,DESC:true);
                if (messages != null)
                    ChatDetails.Messages = messages.ToList();
            }
            return ChatDetails;
        }
    }
}
