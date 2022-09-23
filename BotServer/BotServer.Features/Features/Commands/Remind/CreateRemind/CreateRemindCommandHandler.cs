using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using BotServer.Features.BackgroundJobs.Remind;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Remind.CreateRemind
{
    public class CreateRemindCommandHandler : IRequestHandler<CreateRemindCommand, RemindModel>
    {
        private readonly IBaseRepository _baseRepostory;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accesor;
        private readonly UserManager<User> _userManager;
        //private readonly IBGJobRemind _bGJobRemind;


        public CreateRemindCommandHandler(IBaseRepository baseRepostory,
            IMapper mapper,
            IHttpContextAccessor accesor,
            UserManager<User> userManager
            //IBGJobRemind bGJobRemind
            )
        {
            _userManager = userManager;
            _baseRepostory = baseRepostory;
            _mapper = mapper;
            _accesor = accesor;
            //_bGJobRemind = bGJobRemind;
        }

        public async Task<RemindModel> Handle(CreateRemindCommand request, CancellationToken cancellationToken)
        {
            
            var avtorId =_accesor.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (avtorId != null) {
                var user = await _userManager.FindByIdAsync(avtorId);
                RemindModel model = _mapper.Map<RemindModel>(request);

                model.id = Guid.NewGuid().ToString();
                model.AvtorId = avtorId;
                model.IsDeleted = false;
                model.Created = DateTime.Now;

                //var delay = DateTime.Now - request.RemindAtTime;

                var res = await _baseRepostory.Create(model);
                if (res != null) {
                     //await _bGJobRemind.SetBgJobRemind(res);
                     await _baseRepostory.SaveChangesAsync();
                }
                return res;
            }
            return null;
        }

        //private static Action SendToVk(User user, RemindModel remind)
        //{
            
        //}
    }
}
