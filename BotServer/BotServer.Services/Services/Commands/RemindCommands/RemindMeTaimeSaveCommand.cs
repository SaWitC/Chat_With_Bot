using BotServer.Application.Services.Commands;
using BotServer.Data.Attributes;
using BotServer.Features.Features.Commands.Remind.CreateRemind;
using MediatR;
using System.Text.RegularExpressions;

namespace BotServer.Services.Services.Commands.RemindCommands
{
    [Service]
    public class RemindMeTaimeSaveCommand : ICommandHandler
    {
        private readonly IMediator _mediatr;
        public RemindMeTaimeSaveCommand(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public bool CanProcess(ICommand command)
        {
            if (Regex.IsMatch(command.CommandString, @"[0-9]{4}[-| |:|.]*[0-9]{2}[-| |:|.]*[0-9]{2}[-| |:|.]*[0-9]{2}[-| |:|.]*[0-9]{2}"))
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
                return $"❗❗❗write remind message '{date} message'❗❗❗";
            }

            if (DateTime.Now.AddMinutes(1) < date)
            {
                var createRemindCommand = new CreateRemindCommand();
                createRemindCommand.RemindMessage = remndMessage;
                createRemindCommand.RemindAtTime = date;
                var res = await _mediatr.Send(createRemindCommand);

                return $"Correct Date {res.RemindAtTime} saved remind {res.RemindMessage}";
            }
            else
            {
                return "incorrect Date or time (correct time = curent time + 1 and more minutes )";
            }
        }
    }
}
