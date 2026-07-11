using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Referrals;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class ReferralsController(IQueryDispatcher queryDispatcher) : BaseController
{
    [HttpGet]
    [Authorize(Roles = "Admin,Support")]
    public async Task<ActionResult<PaginatedResult<ReferralDto>>> GetAll([FromQuery] GetReferralsQuery query)
    {
        var result = await queryDispatcher.QueryAsync(query);
        return OkOrNotFound(result);
    }
}
