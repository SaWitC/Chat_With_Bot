using BotServer.Application.CustomHTTPClients;
using BotServer.Application.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BotServer.Services.Services.Commands.WikiCommand
{
    public class GetArticleLinks : ICommandHandler
    {
        private readonly IWikiHttpClient _wikiHttpClient;
        public GetArticleLinks(IWikiHttpClient wikiHttpClient)
        {
            _wikiHttpClient= wikiHttpClient;
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
            var requeststring = Regex.Replace(command.CommandString, "(wiki|wikipedia|wikidepia)", "");
            var respmodel = await _wikiHttpClient.GetLinks(requeststring);

            if (respmodel != null)
            {
                string resp = $"i gona found:";
                foreach (var x in respmodel.query.pages)
                {
                    resp += $"</br><a href={wikiDomain+x.title}>{x.title}</a>";
                }
                return resp;
            }
            return "i can not found nothin";
        }
    }
}
