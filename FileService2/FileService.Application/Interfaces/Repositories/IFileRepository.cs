using FileServer.Domain.Models.File;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FileServer.Application.Interfaces.Repositories
{
    public interface IFileRepository
    {

        public Task<int> SaveChangesAsync();
        public Task<IEnumerable<string>> GetAllFilesTitlesByuserIdAsync(string userId);
        public Task<IEnumerable<FileModel>> GetAllFilesByuserIdAsync(string userId);

        public Task<IEnumerable<string>> GetFilesTitlesByuserIdAsync(string userId, int page = 0, int size = 5);
        public Task<EntityEntry<FileModel>> SaveNewFileAsync(FileModel fileModel);
        public Task<EntityEntry<FileModel>> RemoveFileAsync(string fileTitle);
    }
}
