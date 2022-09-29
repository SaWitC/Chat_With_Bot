using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Commands.Account.UpdatePersonalDataCommand
{
    public class UpdatePersonalDataCommand : IRequest<User>
    {
        public string Id { get; set; }
        public bool SendToVk { get; set; }
    }
}
