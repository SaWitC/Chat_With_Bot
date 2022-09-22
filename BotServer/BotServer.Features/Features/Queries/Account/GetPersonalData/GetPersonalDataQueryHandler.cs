using BotServer.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Account.GetPersonalData
{
    public class GetPersonalDataQueryHandler:IRequestHandler<GetPersonalDataQuery,User>
    {
        private readonly UserManager<User> _userManager;

        public GetPersonalDataQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Handle(GetPersonalDataQuery request, CancellationToken cancellationToken)
        {
            var user =await _userManager.FindByIdAsync(request.UserId);
            return user;
        }
    }
}
