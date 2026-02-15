using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Shared.Helpers;
using ScrapDealer.Shared.ModuleExtensions;
namespace ScrapDealer.Api.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected string AccessToken => Request.GetAccessToken();

        protected virtual Guid UserId => ClaimHelper.GetClaim<Guid>(this.AccessToken, "UserId");

        protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
            => result is null ? NotFound() : Ok(new { data = result });

        protected OkObjectResult BaseOk(string? message = null) => Ok(message ?? "عملیات با موفقیت انجام شد.");
        protected ActionResult<TResult> BaseObjectOk<TResult>(TResult result) => Ok(result);
    }
}
