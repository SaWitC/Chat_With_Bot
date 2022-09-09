using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Messages.GetMessagesQuery
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IEnumerable<MessageModel>>
    {
        private readonly ISelectRepository _selectRepository;
        public GetMessagesQueryHandler(ISelectRepository selectRepository)
        {
            _selectRepository = selectRepository;
        }

        public async Task<IEnumerable<MessageModel>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var res = _selectRepository.SelectWithSortByTimeByParentId<ChatModel,MessageModel>(request.id,page:request.page,DESC:true);
            return res;
        }
    }
}
