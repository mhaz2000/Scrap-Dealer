using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Addresses;
using ScrapDealer.Application.DTO.External;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController(ICommandDispatcher _dispatcher) : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<AddressMapIRResponse>> Post([FromBody] LocationCommand command)
        {
            var response = await _dispatcher.DispatchAsync<LocationCommand, AddressMapIRResponse>(command);
            return OkOrNotFound(response);
        }
    }
}
