using AutoFixture;
using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Data.MapperProfiles;
using BotServer.Domain.Models;
using BotServer.Domain.Models.Short;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BotServer.Features.Tests.Queries.Chat.getChatByIdTest
{
    public class BaseFixture
    {
        public IMapper Mapper { get; private set; }
        public Mock<IBaseRepository> BaseRepository { get; private set; }
        public Mock<IMessageRepository> MessageRepository { get; private set; }
        public Mock<ISelectRepository> SelectRepository { get; private set; }
        public Mock<IMediator> Mediator { get; private set; }
        public Fixture Fixture { get; private set; } = new Fixture();
        public static List<T> GenerateEllements<T,Customisation>(Fixture Fixture,int count) 
            where Customisation :class, ICustomization,new()
        {
            var res =Fixture.Customize(new Customisation()).Build<T>().CreateMany(count).ToList();
            return res;
        }

        public BaseFixture()
        {
            MessageRepository = GetMockOfMessageRepository();
            BaseRepository = GetMockOfBaseRepository();
            SelectRepository = GetMockOfSelectRepository();

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

        public virtual Mock<IBaseRepository> GetMockOfBaseRepository()
        {
            var mock =new Mock<IBaseRepository>();
            var chatModel = Fixture.Build<ChatModel>().Without(o => o.Messages).With(o => o.id, "1").Create();
            mock.Setup(a => a.GetByid<ChatModel>("1")).Returns(async () => (chatModel));

            return mock;
        }

        public virtual Mock<IMessageRepository> GetMockOfMessageRepository()
        {
            var mock = new Mock<IMessageRepository>();
            var messages = Fixture.Build<MessageShortModel>().CreateMany(5);
            mock.Setup(a => a.SelectWithSortByTimeByParentId("1", 0, 5, true)).Returns(() => messages);
            return mock;
        }

        public virtual Mock<ISelectRepository> GetMockOfSelectRepository()
        {
            var mock = new Mock<ISelectRepository>();
            mock.Setup(a => a.CountPagesWithParent<MessageModel>(5, "1")).Returns(1);
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
