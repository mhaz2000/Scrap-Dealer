namespace ScrapDealer.Application.Services
{
    public interface IFileStorageService
    {
        Task<Guid> UploadAsync(MemoryStream fileStream, string originalFileName, string contentType);
        Task<(Stream Stream, string originalFileName, string ContentType)> DownloadAsync(Guid fileId);
    }
}
