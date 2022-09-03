using BotServer.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Chat.DeleteChatCommand
{
    public class DeleteChatCommandHandler : IRequestHandler<DeleteChatCommand, bool>
    {
        public IBaseRepository _baseRepository;
        public DeleteChatCommandHandler(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task<bool> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
