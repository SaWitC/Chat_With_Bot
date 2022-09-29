using AutoFixture;
using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Data.MapperProfiles;
using BotServer.Domain.Models;
using MediatR;
using Moq;

namespace BotServer.Features.Tests.Queries.Chat.GetMyShatsTest
{
    public class BaseFixture
    {
        public IMapper Mapper { get; private set; }

        public Mock<IChatRepository> ChatRepository { get; private set; }
        public Mock<IMediator> Mediator { get; private set; }
        public Fixture Fixture { get; private set; } = new Fixture();
        public static List<T> GenerateEllements<T, Customisation>(Fixture Fixture, int count)
            where Customisation : class, ICustomization, new()
        {
            var res = Fixture.Customize(new Customisation()).Build<T>().CreateMany(count).ToList();
            return res;
        }

        public BaseFixture()
        {
            ChatRepository = GetMockOfChatRepository();

            if (Mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new CustomMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                Mapper = mapper;
            }
        }
        public void Dispose()
        {

        }

        public virtual Mock<IChatRepository> GetMockOfChatRepository()
        {
            var mock = new Mock<IChatRepository>();
            var chats = Fixture.Build<ChatModel>().Without(o => o.Messages).With(o => o.avtorId, "1").CreateMany(5);
            mock.Setup(a => a.GetPageByAvtorId("1", 0, 5)).Returns(() => (chats));

            return mock;
        }


        public virtual void ConfigureMapper<TConfiguration>(TConfiguration configuration) where TConfiguration : Profile, new()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TConfiguration());
            });
            this.Mapper = mappingConfig.CreateMapper();
        }



    }
}
