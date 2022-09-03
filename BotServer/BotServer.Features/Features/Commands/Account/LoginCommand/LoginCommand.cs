using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Account.LoginCommand
{
    public class LoginCommand:IRequest<string>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
