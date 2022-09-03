using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Messages.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, EntityEntry<MessageModel>>
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;

        public SendMessageCommandHandler(IBaseRepository baseRepository,IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<EntityEntry<MessageModel>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<MessageModel>(request.SendMessageDTO);
                entity.Created = DateTime.Now;
                entity.id = Guid.NewGuid().ToString();
                var res = await _baseRepository.Create<ChatModel, MessageModel>(request.SendMessageDTO.ParentId, entity);
                if (res != null)
                    await _baseRepository.SaveChangesAsync();
                return res;
            }
        }
    }
}
