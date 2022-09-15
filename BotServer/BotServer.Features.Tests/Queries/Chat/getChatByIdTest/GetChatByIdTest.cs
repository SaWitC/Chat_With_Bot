using AutoFixture;
using AutoMapper;
using BotServer.Application.Repositories;
using BotServer.Data.MapperProfiles;
using BotServer.Domain.Models;
using BotServer.Domain.Models.Short;
using BotServer.Features.Features.Queries.Chat.GetChatById;
using BotServer.Features.Features.Queries.Chat.GetMyChats;
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
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<BaseFixture>
    {
    }


    [Collection("Database collection")]
    public class GetChatByIdTest
    {
        private readonly BaseFixture Basefixture;

        public GetChatByIdTest(BaseFixture Basefixture)
        {
            this.Basefixture = Basefixture;
        }

        [Fact]
        public async Task Must_Try_Return_chat()
        {
            //arrange
            string id = "1";

            GetChatByIdQuery createChatCommand = new GetChatByIdQuery();
            createChatCommand.id = id;
            
            GetChatByIdQueryHandler handler = new GetChatByIdQueryHandler(
                baseRepository:Basefixture.BaseRepository.Object,
                selectRepository: Basefixture.SelectRepository.Object,
                messageRepository: Basefixture.MessageRepository.Object,
                mapper: Basefixture.Mapper);
            //Act
            var res = await handler.Handle(createChatCommand, new System.Threading.CancellationToken());
            
            //Assert

            Assert.NotNull(res);
            Assert.NotEqual(0,res.Page);
            Assert.NotEqual(0,res.Messages.Count());
        }

        [Fact]

        public async Task Myst_Return_null()
        {
            //arrange
            string id = "2";

            GetChatByIdQuery createChatCommand = new GetChatByIdQuery();
            createChatCommand.id = id;

            GetChatByIdQueryHandler handler = new GetChatByIdQueryHandler(
                baseRepository: Basefixture.BaseRepository.Object,
                selectRepository: Basefixture.SelectRepository.Object,
                messageRepository: Basefixture.MessageRepository.Object,
                mapper: Basefixture.Mapper);
            //Act
            var res = await handler.Handle(createChatCommand, new System.Threading.CancellationToken());

            //Assert
            Assert.Null(res);
        }
    }
}
