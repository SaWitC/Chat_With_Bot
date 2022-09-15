using AutoMapper;
using BotServer.Domain.Models;
using BotServer.Domain.Models.Details;
using BotServer.Domain.Models.Short;
using BotServer.Features.Features.Commands.Chat.CreateChatCommand;
using BotServer.Features.Features.Commands.Chat.UpdateChatCommand;
using BotServer.Features.Features.Commands.Messages.SendMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Data.MapperProfiles
{
    public class CustomMapperProfile: AutoMapper.Profile
    {
        public CustomMapperProfile()
        {
            CreateMap<ChatModel, CreateChatCommand>().ReverseMap().ForMember(x=>x.Messages,s=>s.Ignore());
            //CreateMap<CreateChatCommand, ChatModel>();

            CreateMap<ChatModel, UpdateChatCommand>().ReverseMap().ForMember(x => x.Messages, s => s.Ignore());
            //CreateMap<UpdateChatCommand, ChatModel>();

            //CreateMap<ChatDetailsModel, ChatModel>();
            CreateMap<ChatModel, ChatDetailsModel>().ReverseMap().ForMember(x => x.Messages, s => s.Ignore());

            CreateMap<MessageModel, SendMessageDTO>().ReverseMap();

            CreateMap<MessageShortModel, MessageModel>().ReverseMap();
            //CreateMap<SendMessageDTO, MessageModel>();


        }
    }
}
