using FileServer.Application.Interfaces.Repositories;
using FileServer.Data.Data;
using FileServer.Domain.Models.File;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FileServer.Data.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetAllFilesTitlesByuserIdAsync(string userId)
        {
            try
            {
                return await _context.Files.Where(x => x.UserId == userId).Select(x => x.FileTitle).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<string>> GetFilesTitlesByuserIdAsync(string userId, int page = 0, int size = 5)
        {
            try
            {
                return await _context.Files.Where(x => x.UserId == userId).Skip(page * size).Take(size).Select(x => x.FileTitle).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EntityEntry<FileModel>> RemoveFileAsync(string fileTitle)
        {
            if (!string.IsNullOrEmpty(fileTitle))
            {
                var file = await _context.Files.FirstOrDefaultAsync(x => x.FileTitle == fileTitle);
                if (file != null)
                {
                    return _context.Files.Remove(file);
                }
                else
                {
                    throw new Exception("file not found");
                }
            }
            else
            {
                throw new Exception("fileName can't be null");
            }
        }

        public async Task<EntityEntry<FileModel>> SaveNewFileAsync(FileModel fileModel)
        {
            if (fileModel != null)
            {
                return await _context.Files.AddAsync(fileModel);
            }
            throw new Exception(" file can no be null");
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FileModel>> GetAllFilesByuserIdAsync(string userId)
        {
            return await _context.Files.Where(o => o.UserId == userId).ToListAsync();
        }
    }
}
