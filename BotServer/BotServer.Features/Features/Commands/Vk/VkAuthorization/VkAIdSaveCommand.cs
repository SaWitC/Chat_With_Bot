using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Vk.VkAuthorization
{
    public class VkAIdSaveCommand:IRequest<long?>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
    }
}
