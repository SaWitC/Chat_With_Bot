using BotServer.Application.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BotServer.Features.Features.Commands.Remind.CreateRemind;
using MediatR;

namespace BotServer.Services.Services.Commands.RemindCommands
{
    public class RemindMeTaimeSaveCommand : ICommandHandler
    {
        private readonly IMediator _mediatr;
        public RemindMeTaimeSaveCommand(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public bool CanProcess(ICommand command)
        {
            if (Regex.IsMatch(command.CommandString, @"[0-9]{4}[-| |:|.]*[0-9]{2}[-| |:|.]*[0-9]{2}[-| |:|.]*[0-9]{2}[-| |:|.]*[0-9]{2} [а-яa-zА-ЯA-Z0-9. ]{1,}"))
                return true;
            return false;
        }

        public async Task<string> ProcessCommand(ICommand command)
        {
            Match match = Regex.Match(command.CommandString.ToLower(), @"[0-9]{4}[-| |:|.]*[0-9]{2}[-| |:|.]*[0-9]{2}[-| |:|.]*[0-9]{2}[-| |:|.]*[0-9]{2}");
            DateTime date = DateTime.Parse(match.Groups[0].Value);
            var remndMessage = command.CommandString.Replace(match.Groups[0].Value, "");
            if (string.IsNullOrEmpty(remndMessage))
            {
                return $"write remind message '{date} message'";
            }

            if (DateTime.Now.AddMinutes(6) < date)
            {
                var createRemindCommand = new CreateRemindCommand();
                createRemindCommand.RemindMessage = remndMessage;
                createRemindCommand.RemindAtTime = date;
                var res =await _mediatr.Send(createRemindCommand);

                return $"Correct Date {res.RemindAtTime} saved remind {res.RemindMessage}";
               
            }
            else
            {
                return "incorrect Date or time (correct time = curent time + 6 and more minutes )";
            }
        }
    }
}
