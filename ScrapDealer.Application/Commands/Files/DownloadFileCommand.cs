using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Files
{
    public record DownloadFileCommand(Guid Id) : ICommand<(Stream stream, string originalFileName, string contentType)>;

}
