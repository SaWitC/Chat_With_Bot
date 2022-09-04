using AutoMapper;
using BotServer.Domain.Models;
using BotServer.Features.Features.Commands.Chat.CreateChatCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Data.MapperProfiles
{
    class ChatProfile:Profile
    {
        public ChatProfile()
        {
            CreateMap<ChatModel, CreateChatDTO>();
            CreateMap<CreateChatDTO, ChatModel>();
        }
    }
}
