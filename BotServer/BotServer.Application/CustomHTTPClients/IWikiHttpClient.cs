using BotServer.Domain.HttResponseModels;

namespace BotServer.Application.CustomHTTPClients
{
    public interface IWikiHttpClient
    {

        public Task<WikiModels.Rootobject> GetLinks(string requestText);
    }
}
