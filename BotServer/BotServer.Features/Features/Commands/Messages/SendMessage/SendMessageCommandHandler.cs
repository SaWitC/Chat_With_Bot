using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Application.Services.Commands;
using BotServer.Domain.Models;
using BotServer.Services.Services.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Messages.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, MessageModel>
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        //private readonly IEnumerable<ICommandHandler> _commandHandlers;

        public SendMessageCommandHandler(IBaseRepository baseRepository,IMapper mapper/*,IEnumerable<ICommandHandler> commandHandlers*/)
        {
            //_commandHandlers = commandHandlers;
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<MessageModel> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<MessageModel>(request.SendMessageDTO);
                if (entity != null)
                {
                    entity.Created = DateTime.Now;
                    entity.id = Guid.NewGuid().ToString();
                    var res = await _baseRepository.Create<ChatModel, MessageModel>(request.SendMessageDTO.ParentId, entity);
                    if (res != null)
                        await _baseRepository.SaveChangesAsync();


                    return res.Entity;
                }
                return null;

            }
            catch
            {
                //logg
                return null;
            }
        }
    }
}
