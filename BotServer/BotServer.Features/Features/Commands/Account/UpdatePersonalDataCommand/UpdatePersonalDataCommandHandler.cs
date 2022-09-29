using AutoMapper;
using BotServer.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BotServer.Features.Features.Commands.Account.UpdatePersonalDataCommand
{
    public class UpdatePersonalDataCommandHandler : IRequestHandler<UpdatePersonalDataCommand, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UpdatePersonalDataCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<User> Handle(UpdatePersonalDataCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user != null)
            {
                user.SendToVk = request.SendToVk;

            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return user;
            }
            return null;
        }
    }
}
