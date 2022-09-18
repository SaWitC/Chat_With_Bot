using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        public CreateRemindCommandHandler(IBaseRepository baseRepostory,IMapper mapper, IHttpContextAccessor accesor)
        {
            _baseRepostory = baseRepostory;
            _mapper = mapper;
            _accesor = accesor;
        }

        public async Task<RemindModel> Handle(CreateRemindCommand request, CancellationToken cancellationToken)
        {
            var avtorId =_accesor.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            RemindModel model = _mapper.Map<RemindModel>(request);

            model.id = Guid.NewGuid().ToString();
            model.AvtorId = avtorId;
            model.IsDeleted = false; 
            model.Created = DateTime.Now;

            var res = await _baseRepostory.Create(model);
            if(res!=null)
                await _baseRepostory.SaveChangesAsync();
            return res;

        }
    }
}
