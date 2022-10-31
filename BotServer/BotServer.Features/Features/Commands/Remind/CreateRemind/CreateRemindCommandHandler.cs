using AutoMapper;
using BotServer.Application.DataServices;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using BotServer.Features.BackgroundJobs.Remind;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace BotServer.Features.Features.Commands.Remind.CreateRemind
{
    public class CreateRemindCommandHandler : IRequestHandler<CreateRemindCommand, RemindModel>
    {
        private readonly IBaseRepository _baseRepostory;
        private readonly IMapper _mapper;
        private readonly IHttpContextService _httpContextService;
        private readonly UserManager<User> _userManager;
        private readonly IBGJobRemind _bGJobRemind;
        public CreateRemindCommandHandler(
            IBaseRepository baseRepostory,
            IMapper mapper,
            IHttpContextService httpContextService,
            UserManager<User> userManager,
            IBGJobRemind bGJobRemind
            )
        {
            _userManager = userManager;
            _baseRepostory = baseRepostory;
            _mapper = mapper;
            _httpContextService = httpContextService;
            _bGJobRemind = bGJobRemind;
        }

        public async Task<RemindModel> Handle(CreateRemindCommand request, CancellationToken cancellationToken)
        {

            var avtorId = _httpContextService.GetCurentUserId();
            if (avtorId != null)
            {
                var user = await _userManager.FindByIdAsync(avtorId);
                RemindModel model = _mapper.Map<RemindModel>(request);

                model.id = Guid.NewGuid().ToString();
                model.AvtorId = avtorId;
                model.IsDeleted = false;
                model.Created = DateTime.Now;

                var res = await _baseRepostory.Create(model);
                if (res != null)
                {
                    var DleayedTasSetResponse = await _bGJobRemind.SetBgJobRemind(res);
                    await _baseRepostory.SaveChangesAsync();
                }
                return res;
            }
            return null;
        }
    }
}
