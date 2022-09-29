using BotServer.Domain.Models;
using BotServer.Features.Features.Queries.Chat.GetMyChats;
using Xunit;

namespace BotServer.Features.Tests.Queries.Chat.GetMyShatsTest
{
    public class GetMyChat : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture baseFixture;
        //[SetUp]
        public GetMyChat(BaseFixture baseFixture)
        {
            this.baseFixture = baseFixture;
        }

        //[TestCase("first")]
        [Fact]
        public async Task must_return_5Chats()
        {
            //arrange
            string avtorId = Guid.Empty.ToString();

            GetMyChatsQuery createChatCommand = new GetMyChatsQuery();
            createChatCommand.AvtorId = "1";
            GetMyChatsQueryHandler handler = new GetMyChatsQueryHandler(baseFixture.ChatRepository.Object);
            //Act
            var res = await handler.Handle(createChatCommand, new System.Threading.CancellationToken());
            //Assert

            Assert.NotNull(res);
            Assert.Equal<int>(5, res.Count());
        }

        [Fact]
        public async Task must_return_EmptyList()
        {
            //arrange       
            GetMyChatsQuery createChatCommand = new GetMyChatsQuery();
            createChatCommand.AvtorId = "2";
            GetMyChatsQueryHandler handler = new GetMyChatsQueryHandler(baseFixture.ChatRepository.Object);
            //Act
            var res = await handler.Handle(createChatCommand, new System.Threading.CancellationToken());
            //Assert

            Assert.Equal(new List<ChatModel>(), res);
        }
    }
}
