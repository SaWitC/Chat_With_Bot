using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Account.RegistrationCommand
{
    public class RegistrationCommand:IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPass { get; set; }
        public string Email { get; set; }
    }
}
