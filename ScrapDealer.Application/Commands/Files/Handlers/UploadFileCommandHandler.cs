using ScrapDealer.Application.Services;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Commands.Files.Handlers
{
    internal class UploadFileCommandHandler : ICommandHandler<UploadFileCommand, Guid>
    {
        private readonly IFileStorageService _fileStorage;

        public UploadFileCommandHandler(IFileStorageService fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<Guid> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            request.File.Position = 0;
            return await _fileStorage.UploadAsync(request.File, request.FileName, "application/octet-stream", request.bucketName);
        }
    }
}
