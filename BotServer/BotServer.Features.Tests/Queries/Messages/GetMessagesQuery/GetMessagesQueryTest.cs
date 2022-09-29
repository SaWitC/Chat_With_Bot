using AutoFixture;
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using BotServer.Features.Features.Queries.Messages.GetMessagesQuery;
using Moq;
using Xunit;

namespace BotServer.Features.Tests.Queries.Messages.GetMessagesQuery
{
    public class GetMessagesQueryTest
    {
        //[TestCase("1",0,true)]
        [Fact]
        public async Task must_return_messages()
        {
            //arrange
            Fixture fixture = new Fixture();
            var message = fixture.Build<MessageModel>().With(x => x.ParentId, "1").Without(x => x.chat).Create();
            IEnumerable<MessageModel> messages = new List<MessageModel>()
            {
                message
            };

            var SelectRepoMock = new Mock<ISelectRepository>();
            SelectRepoMock.Setup(x => x.SelectWithSortByTimeByParentId<ChatModel, MessageModel>("1", 0, 5, false))
                .Returns(messages);

            BotServer.Features.Features.Queries.Messages.GetMessagesQuery.GetMessagesQuery getMessagesQuery = new();
            getMessagesQuery.id = "1";
            getMessagesQuery.page = 0;

            GetMessagesQueryHandler messagesQueryHandler = new GetMessagesQueryHandler(SelectRepoMock.Object);
            //act
            var res = await messagesQueryHandler.Handle(getMessagesQuery, new CancellationToken());

            //assert

            Assert.NotNull(res);
            Assert.Equal(messages.Count(), res.Count());
        }

        //[Test]
        //public async Task must_return_null()
        //{
        //        //arrange
        //    Fixture fixture = new Fixture();
        //    var message = fixture.Build<MessageModel>().With(x => x.ParentId, "1").Without(x => x.chat).Create();
        //    IEnumerable<MessageModel> messages = new List<MessageModel>()
        //    {
        //    message
        //    };

        //    var SelectRepoMock = new Mock<ISelectRepository>();
        //    SelectRepoMock.Setup(x => x.SelectWithSortByTimeByParentId<ChatModel, MessageModel>("1", 0, 5, false))
        //        .Returns(messages);

        //    BotServer.Features.Features.Queries.Messages.GetMessagesQuery.GetMessagesQuery getMessagesQuery = new();
        //    getMessagesQuery.id = null;
        //    getMessagesQuery.page = -1;

        //    GetMessagesQueryHandler messagesQueryHandler = new GetMessagesQueryHandler(SelectRepoMock.Object);
        //    //act
        //    var res = await messagesQueryHandler.Handle(getMessagesQuery, new CancellationToken());

        //    //assert
        //    Assert
        //    //Assert.Throws<ValidationException>(async() => await messagesQueryHandler.Handle(getMessagesQuery, new CancellationToken()));

        //}
    }
}
