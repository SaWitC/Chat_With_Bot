using FileServer.Domain.Models.File;
using Microsoft.EntityFrameworkCore;

namespace FileServer.Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<FileModel> Files { get; set; }
    }
}
