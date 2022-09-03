using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.ChatCommands.CreateChatCommand
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, EntityEntry<ChatModel>>
    {

        private readonly IBaseRepository _baseRepository;

        public CreateChatCommandHandler(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<EntityEntry<ChatModel>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
