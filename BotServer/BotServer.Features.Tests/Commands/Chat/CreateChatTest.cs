using AutoFixture;
using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Data.MapperProfiles;
using BotServer.Domain.Models;
using BotServer.Features.Features.Commands.Chat.CreateChatCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using BotServer.Application.DataServices;
using Xunit;

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
        private async Task<ChatModel> returnChatmodel(ChatModel chatModel)
        {
            return chatModel;
        }

        [Fact]
        //[Theory("fisrt")]

        //public async Task CreateChat()
        //{
        //    //arrange
        //    string title = "firstchat";
        //    Fixture fixture = new Fixture();

        //    var chatModel = fixture.Build<ChatModel>().Without(o => o.Messages).With(o => o.Title, title).Create();

        //    var mediator = new Mock<IMediator>();
        //    var baseRepo = new Mock<IBaseRepository>();
        //    baseRepo.Setup(a => a.Create<ChatModel>(chatModel)).Returns(returnChatmodel(chatModel));

        //    var httpContext = new Mock<IHttpContextService>();

        //    IEnumerable<Claim> claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString())
        //    };
        //    //httpContext.Object.HttpContext.User.AddIdentity(new ClaimsIdentity(claims));


        //    CreateChatCommand createChatCommand = new CreateChatCommand();
        //    createChatCommand.Title = title;
        //    CreateChatCommandHandler handler = new CreateChatCommandHandler(baseRepo.Object, _mapper, httpContext.Object);
        //    //Act
        //    var res = await handler.Handle(createChatCommand, new System.Threading.CancellationToken());

        //    //Assert

        //    Assert.Equal(res, chatModel);

        //}

    }
}
