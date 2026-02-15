using ScrapDealer.Application.Services;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Files.Handlers
{
    internal class DownloadFileHandler : ICommandHandler<DownloadFileCommand, (Stream stream, string originalFileName, string contentType)>
    {
        private readonly IFileStorageService _fileStorage;

        public DownloadFileHandler(IFileStorageService fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<(Stream stream, string originalFileName, string contentType)> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {
            return await _fileStorage.DownloadAsync(request.Id, request.bucketName);
        }
    }


}
