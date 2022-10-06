namespace FileServer.Application.Interfaces.Repositories
{
    public interface IBaseRepository
    {
        public Task<int> SaveChangesAsync();
    }
}
