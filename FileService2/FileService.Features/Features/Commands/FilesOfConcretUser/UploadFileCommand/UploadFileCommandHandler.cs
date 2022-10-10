using FileServer.Application.Interfaces.Azure;
using FileServer.Application.Interfaces.Repositories;
using FileServer.Domain.Models.File;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace FileServer.Features.Features.Commands.FilesOfConcretUser.UploadFileCommand
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, bool>
    {
        private readonly IBlobService _blobService;
        private readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;
        public UploadFileCommandHandler(IBlobService blobService, IFileRepository fileRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _fileRepository = fileRepository;
            _blobService = blobService;
        }

        public async Task<bool> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            await _blobService.UploadFileBlobAsync(request.UserId, request.file);

            FileModel fileModel = new FileModel();
            fileModel.Created = DateTime.Now;
            fileModel.FileTitle = request.file.FileName;
            fileModel.FileType = request.file.ContentType;
            fileModel.UserId = request.UserId;
            fileModel.BlobName = _configuration["Azure:blobsofusers"];
            await _fileRepository.SaveNewFileAsync(fileModel);

            await _fileRepository.SaveChangesAsync();

            return true;
        }
    }
}
