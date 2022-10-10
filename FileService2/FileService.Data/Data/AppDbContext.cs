using FileServer.Domain.Models.File;
using FileService.Data.DataConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FileServer.Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<FileModel> Files { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FileModelConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
