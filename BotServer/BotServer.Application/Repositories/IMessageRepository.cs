using BotServer.Domain.Models.Short;

namespace BotServer.Application.Repositories
{
    public interface IMessageRepository
    {
        public IEnumerable<MessageShortModel> SelectWithSortByTimeByParentId(string parentId, int page = 0, int size = 5, bool DESC = false);
    }
}
