using BotServer.Application.CustomHTTPClients;
using BotServer.Application.Services.Commands;
using BotServer.Data.Attributes;
using System.Text.RegularExpressions;

namespace BotServer.Services.Services.Commands.WikiCommand
{
    [Service]
    public class GetArticleLinks : ICommandHandler
    {
        private readonly IWikiHttpClient _wikiHttpClient;
        public GetArticleLinks(IWikiHttpClient wikiHttpClient)
        {
            _wikiHttpClient = wikiHttpClient;
        }
        public bool CanProcess(ICommand command)
        {
            if (command.CommandString.ToLower().Contains("wiki") ||
                command.CommandString.ToLower().Contains("wikipedia") ||
                command.CommandString.ToLower().Contains("wikidepia"))
                return true;
            return false;
        }

        public async Task<string> ProcessCommand(ICommand command)
        {
            string wikiDomain = "https://en.wikipedia.org/wiki/";
            var requestString = Regex.Replace(command.CommandString, "(wiki|wikipedia|wikidepia)", "");
            var respModel = await _wikiHttpClient.GetLinks(requestString);

            if (respModel != null)
            {
                string resp = $"i gonna found:";
                foreach (var x in respModel.query.pages)
                {
                    resp += $"</br><a href={wikiDomain + x.title}>{x.title}</a>";
                }
                return resp;
            }
            return "i can not found nothing";
        }
    }
}
