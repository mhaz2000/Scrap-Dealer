using Minio;
using Minio.DataModel.Args;
using ScrapDealer.Application.Services;

public class MinioFileStorageService : IFileStorageService
{
    private readonly IMinioClient _client;

    public MinioFileStorageService(IMinioClient client)
    {
        _client = client;
    }

    private async Task EnsureBucketExists(string bucketName)
    {
        var exists = await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        if (!exists)
        {
            await _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
        }
    }

    public async Task<Guid> UploadAsync(MemoryStream fileStream, string originalFileName, string contentType, string bucketName)
    {
        await EnsureBucketExists(bucketName);

        var fileId = Guid.NewGuid();
        var fileName = $"{fileId}.dat";

        var metadata = new Dictionary<string, string>
        {
            { "original-filename", originalFileName }
        };

        await _client.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType)
            .WithHeaders(metadata));

        return fileId;
    }

    public async Task<(Stream Stream, string originalFileName, string ContentType)> DownloadAsync(Guid fileId, string bucketName)
    {
        var fileName = $"{fileId}.dat";
        var memoryStream = new MemoryStream();

        var stat = await _client.StatObjectAsync(new StatObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName));

        await _client.GetObjectAsync(new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithCallbackStream(stream => stream.CopyTo(memoryStream)));

        memoryStream.Position = 0;

        var originalFileName = stat.MetaData.TryGetValue("original-filename", out var value)
            ? value ?? $"{fileId}.dat"
            : $"{fileId}.dat";

        return (memoryStream, originalFileName, stat.ContentType);
    }
}
