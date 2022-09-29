using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Queries.Account.GetPersonalData
{
    public class GetPersonalDataQuery : IRequest<User>
    {
        public string UserId { get; set; }
    }
}
