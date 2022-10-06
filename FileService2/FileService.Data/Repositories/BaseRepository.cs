using FileServer.Data.Data;

namespace FileServer.Data.Repositories
{
    public abstract class BaseRepository
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
