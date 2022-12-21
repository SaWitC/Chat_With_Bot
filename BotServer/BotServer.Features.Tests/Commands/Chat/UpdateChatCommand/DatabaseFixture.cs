using AutoMapper;
using BotServer.Data.MapperProfiles;
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
}
