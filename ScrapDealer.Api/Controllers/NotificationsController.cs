using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Notifications;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Notifications;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : BaseController
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] AddNotificationCommand command)
        {
            await commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateNotificationCommand command)
        {
            await commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await commandDispatcher.DispatchAsync(new DeleteNotificationCommand(id));
            return BaseOk();
        }

        [HttpPut("Seen/{id:guid}")]
        [Authorize(Roles = "Admin,Support")]
        public async Task<IActionResult> Seen([FromRoute] Guid id)
        {
            await commandDispatcher.DispatchAsync(new SeenNotificationCommand(id, UserId));
            return BaseOk();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Support")]
        public async Task<ActionResult<PaginatedResult<NotificationDto>>> Get([FromQuery] GetNotificationsQuery query)
        {
            var result = await queryDispatcher.QueryAsync(query with { UserId = UserId});
            return OkOrNotFound(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Support")]
        public async Task<ActionResult<NotificationDto>> GetNotification(Guid id)
        {
            var result = await queryDispatcher.QueryAsync(new GetNotificationQuery(id, UserId));
            return OkOrNotFound(result);
        }
    }
}
