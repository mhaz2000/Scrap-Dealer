using ScrapDealer.Application.Commands.Files;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScrapDealer.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public FilesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Upload(IFormFile file)
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);

            var fileId = await _commandDispatcher.DispatchAsync<UploadFileCommand, Guid>(
                new UploadFileCommand(stream, file.FileName));

            return Ok(fileId);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Download(Guid id)
        {
            var (fileStream, originalFileName, contentType) = await _commandDispatcher
                .DispatchAsync<DownloadFileCommand, (Stream, string, string)>(new DownloadFileCommand(id));

            var fileName = $"{id}.dat";

            Response.Headers.Append("Access-Control-Allow-Headers", "Content-Disposition");
            Response.Headers.Append("X-Content-Type-Options", "nosniff");

            return File(fileStream, contentType, originalFileName);
        }

    }
}
