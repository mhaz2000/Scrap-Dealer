using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ScrapDealer.Application.Commands.InitialBuyers;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.InitialBuyers;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InitialBuyersController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public InitialBuyersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [AllowAnonymous]
    [EnableRateLimiting("InitialBuyerCreate")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInitialBuyerCommand command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return BaseOk();
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<InitialBuyerDto>>> Get([FromQuery] GetInitialBuyersQuery query)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(query));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<InitialBuyerDto>> Get(Guid id)
    {
        var result = await _queryDispatcher.QueryAsync(new GetInitialBuyerQuery(id));
        return OkOrNotFound(result);
    }
}
