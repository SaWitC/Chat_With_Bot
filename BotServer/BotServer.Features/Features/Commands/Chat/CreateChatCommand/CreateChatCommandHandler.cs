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

namespace BotServer.Features.Features.Commands.Chat.CreateChatCommand
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand,ChatModel>
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        public CreateChatCommandHandler(IBaseRepository baseRepository,IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<ChatModel> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<ChatModel>(request.createChatDTO);

            model.id = Guid.NewGuid().ToString();
            model.Created = DateTime.Now;
            
            var res =await _baseRepository.Create<ChatModel>(model);
            await _baseRepository.SaveChangesAsync();
            return res.Entity;
        }
    }
}
