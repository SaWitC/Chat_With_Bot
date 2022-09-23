using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Account.UpdatePersonalDataCommand
{
    public class UpdatePersonalDataCommand:IRequest<User>
    {
        public string Id { get; set; }
        public bool SendToVk { get; set; }
    }
}
