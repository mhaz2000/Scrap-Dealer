using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Shared.Helpers;
using ScrapDealer.Shared.ModuleExtensions;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected string AccessToken => Request.GetAccessToken();

        protected virtual Guid UserId => ClaimHelper.GetClaim<Guid>(this.AccessToken, "UserId");
        protected virtual string UserRole => ClaimHelper.GetClaim<string>(this.AccessToken, "role");

        protected ActionResult OkOrNotFound<TResult>(TResult result)
        {
            if (result is null)
                return NotFound();

            if (result.GetType().IsGenericType &&
                result.GetType().GetGenericTypeDefinition() == typeof(PaginatedResult<>))
            {
                dynamic d = result;
                return Ok(new ApiResponse<object>
                {
                    Data = d.Data,
                    Total = (int)d.TotalCount
                });
            }

            return Ok(new ApiResponse<TResult> { Data = result });
        }

        protected ActionResult BaseOk(string? message = null)
            => Ok(new ApiResponse<string> { Data = message ?? "عملیات با موفقیت انجام شد.", Message = message });

        protected ActionResult BaseObjectOk<TResult>(TResult result)
            => Ok(new ApiResponse<TResult> { Data = result });
    }
}
