namespace BotServer.Application.Repositories
{
    public interface IRemindRepository
    {
        public Task<IEnumerable<RemindModel>> GetActualAndExpiredRemindsByAvtorId(string AvtorId);
        public Task<IEnumerable<RemindModel>> GetActualRemindsByAvtorId(string AvtorId);
        public Task<IEnumerable<RemindModel>> GetExpiredRemindsbyAvorId(string AvtorId);


    }
}
