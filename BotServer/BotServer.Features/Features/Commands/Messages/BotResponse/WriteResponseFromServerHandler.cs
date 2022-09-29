//using BotServer.Application.Repositories;
//using BotServer.Application.Services.Commands;
//using BotServer.Domain.Models;
//using BotServer.Services.Services.Commands;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BotServer.Features.Features.Commands.Messages.BotResponse
//{
//    public class WriteResponseFromServerHandler : IRequestHandler<WriteResponseFromServer, MessageModel>
//    {
//        private readonly IBaseRepository _baseRepository;
//        private readonly IEnumerable<ICommandHandler> _commandHandlers;

//        public WriteResponseFromServerHandler(IEnumerable<ICommandHandler> commandHandlers,IBaseRepository baseRepository)
//        {
//            _baseRepository = baseRepository;
//            _commandHandlers = commandHandlers;
//        }

//        public async Task<MessageModel> Handle(WriteResponseFromServer request, CancellationToken cancellationToken)
//        {
//            var entity = new MessageModel();

//            foreach (var x in _commandHandlers)
//            {
//                if (x.CanProcess(new Command(request.MessageFromUser)))
//                {
//                    entity.text = await x.ProcessCommand(new Command(request.MessageFromUser));
//                    break;
//                }
//            }
//            entity.id = Guid.NewGuid().ToString();
//            entity.IsFromBot = true;
//            entity.avtroId = Guid.Empty.ToString();
//            entity.ParentId = request.ChatId;

//            await _baseRepository.Create<MessageModel>(entity);
//            await _baseRepository.SaveChangesAsync();

//            return entity;         
//        }
//    }
//}
