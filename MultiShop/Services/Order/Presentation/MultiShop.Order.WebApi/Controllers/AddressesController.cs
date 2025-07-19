using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;

namespace MultiShop.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly GetAddressByIdQueryHandler _getAddressByIdQueryHandler;
        private readonly GetAddressQueryHandler _getAddressQueryHandler;
        private readonly CreateAddressCommandHandler _createAddressCommandHandler;
        private readonly UpdateAddressCommandHandler _updateAddressCommandHandler;
        private readonly RemoveAddressCommandHandler _removeAddressCommandHandler;

        public AddressesController(GetAddressByIdQueryHandler getAddressByIdQueryHandler, GetAddressQueryHandler getAddressQueryHandler, CreateAddressCommandHandler createAddressCommandHandler, UpdateAddressCommandHandler updateAddressCommandHandler, RemoveAddressCommandHandler removeAddressCommandHandler)
        {
            _getAddressByIdQueryHandler = getAddressByIdQueryHandler;
            _getAddressQueryHandler = getAddressQueryHandler;
            _createAddressCommandHandler = createAddressCommandHandler;
            _updateAddressCommandHandler = updateAddressCommandHandler;
            _removeAddressCommandHandler = removeAddressCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> AddressList()
        {
            var addresses = await _getAddressQueryHandler.Handle();
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AddressById(int id)
        {
            var address = await _getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand createAddressCommand)
        {
            if (createAddressCommand == null)
            {
                return BadRequest("Invalid address data.");
            }
            await _createAddressCommandHandler.Handle(createAddressCommand);
            return Ok("Address created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommand updateAddressCommand)
        {
            if (updateAddressCommand == null)
            {
                return BadRequest("Invalid address data.");
            }
            await _updateAddressCommandHandler.Handle(updateAddressCommand);
            return Ok("Address updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAddress(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid address ID.");
            }
            await _removeAddressCommandHandler.Handle(new RemoveAddressCommand(id));
            return Ok("Address removed successfully.");
        }
    }
}
