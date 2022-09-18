using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotServer.Features;
using NUnit;
using BotServer.Features.Features.Commands.Chat.CreateChatCommand;
using Moq;
using MediatR;
using BotServer.Application.Repositories;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Xunit;
using AutoFixture;
using BotServer.Data.Data;
using Microsoft.EntityFrameworkCore;
using BotServer.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BotServer.Data.MapperProfiles;
using System.Security.Claims;

namespace BotServer.Features.Tests.Commands.Chat
{
    public class CreateChatTest
    {

        private IMapper _mapper;

        public CreateChatTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new CustomMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
        public async Task<ChatModel> returnChatmodel(ChatModel chatModel)
        {
            return chatModel;
        }

        [Fact]
        //[Theory("fisrt")]
        
        public async Task CreateChat()
        {
            //arrange
            string title = "firstchat";
            Fixture fixture = new Fixture();

            var chatModel = fixture.Build<ChatModel>().Without(o => o.Messages).With(o => o.Title, title).Create();

            var mediator = new Mock<IMediator>();
            var baseRepo = new Mock<IBaseRepository>();
            baseRepo.Setup(a => a.Create<ChatModel>(chatModel)).Returns(returnChatmodel(chatModel));

            var httpContext = new Mock<IHttpContextAccessor>();

            IEnumerable<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString())
            };
            httpContext.Object.HttpContext.User.AddIdentity(new ClaimsIdentity(claims));
            //httpContext.Setup(x => x.HttpContext.User.Claims.Single()).Returns(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));


            CreateChatCommand createChatCommand = new CreateChatCommand();
            createChatCommand.Title = title;
            CreateChatCommandHandler handler = new CreateChatCommandHandler(baseRepo.Object, _mapper, httpContext.Object);
            //Act
            var res = await handler.Handle(createChatCommand, new System.Threading.CancellationToken());

            //Assert

            Assert.Equal(res, chatModel);

        }

    }
}
