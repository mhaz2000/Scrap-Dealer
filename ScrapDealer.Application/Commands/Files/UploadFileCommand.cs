using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Files
{
    public record UploadFileCommand(MemoryStream File, string FileName, string bucketName) : ICommand<Guid>;
}
