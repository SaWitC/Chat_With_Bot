using AutoMapper;
using BotServer.Application.DataServices;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BotServer.Features.Features.Commands.Chat.CreateChatCommand
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, ChatModel>
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextService _httpContextService;

        public CreateChatCommandHandler(IBaseRepository baseRepository, IMapper mapper, IHttpContextService httpContextService)
        {
            _httpContextService = httpContextService;
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<ChatModel> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextService.GetCurentUserId();
            var model = _mapper.Map<ChatModel>(request);

            model.avtorId = userId;
            model.id = Guid.NewGuid().ToString();
            model.Created = DateTime.Now;

            var res = await _baseRepository.Create(model);
            await _baseRepository.SaveChangesAsync();
            return res;
        }
    }
}
