using AutoMapper;
using BotServer.Data.MapperProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BotServer.Features.Tests.Commands.Chat.UpdateChatCommand
{
    public class BaseFixture : IDisposable
    {
        public BaseFixture()
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

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }

        public IMapper _mapper { get; private set; }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<BaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    [Collection("Database collection")]
    public class DatabaseTestClass1
    {
        BaseFixture fixture;

        public DatabaseTestClass1(BaseFixture fixture)
        {
            this.fixture = fixture;
        }
        [Fact]
        public void Test1()
        {
            Assert.NotNull(fixture._mapper);
        }
    }

    [Collection("Database collection")]
    public class DatabaseTestClass2
    {
        // ...
    }
}
