using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BotServer.Features.Features.Commands.Messages.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, MessageModel>
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SendMessageCommandHandler> _logger;

        public SendMessageCommandHandler(IBaseRepository baseRepository, IMapper mapper,ILogger<SendMessageCommandHandler> logger)
        {
            _logger = logger;
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

                    return res;
                }
                return null;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
