using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Commands.Chat.UpdateChatCommand
{
    public class UpdateChatCommandHandler : IRequestHandler<UpdateChatCommand, ChatModel>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository _baseRepository;

        public UpdateChatCommandHandler(IMapper mapper, IBaseRepository baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public async Task<ChatModel> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<ChatModel>(request);

            var res = await _baseRepository.Update<ChatModel>(model, request.oldModelId);
            await _baseRepository.SaveChangesAsync();

            return res;
        }
    }
}
