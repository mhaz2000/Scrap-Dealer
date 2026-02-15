namespace ScrapDealer.Application.Services
{
    public interface IFileStorageService
    {
        Task<Guid> UploadAsync(MemoryStream fileStream, string originalFileName, string contentType, string bucketName);
        Task<(Stream Stream, string originalFileName, string ContentType)> DownloadAsync(Guid fileId, string bucketName);
    }
}
