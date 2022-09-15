using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Chat.CreateChatCommand
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand,ChatModel>
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accesor;

        public CreateChatCommandHandler(IBaseRepository baseRepository,IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            _accesor = httpContextAccessor;
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<ChatModel> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var Id = Guid.Parse(_accesor.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var model = _mapper.Map<ChatModel>(request);

            model.avtorId = Id.ToString();
            model.id = Guid.NewGuid().ToString();
            model.Created = DateTime.Now;
            
            var res =await _baseRepository.Create<ChatModel>(model);
            await _baseRepository.SaveChangesAsync();
            return res;
        }
    }
}
