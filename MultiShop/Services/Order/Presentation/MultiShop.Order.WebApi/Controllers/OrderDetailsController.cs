using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;

namespace MultiShop.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly GetOrderDetailByIdQueryHandler _getOrderDetailByIdQueryHandler;
        private readonly GetOrderDetailQueryHandler _getOrderDetailQueryHandler;
        private readonly CreateOrderDetailCommandHandler _createOrderDetailCommandHandler;
        private readonly UpdateOrderDetailCommandHandler _updateOrderDetailCommandHandler;
        private readonly RemoveOrderDetailCommandHandler _removeOrderDetailCommandHandler;

        public OrderDetailsController(GetOrderDetailByIdQueryHandler getOrderDetailByIdQueryHandler, GetOrderDetailQueryHandler getOrderDetailQueryHandler, CreateOrderDetailCommandHandler createOrderDetailCommandHandler, UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler, RemoveOrderDetailCommandHandler removeOrderDetailCommandHandler)
        {
            _getOrderDetailByIdQueryHandler = getOrderDetailByIdQueryHandler;
            _getOrderDetailQueryHandler = getOrderDetailQueryHandler;
            _createOrderDetailCommandHandler = createOrderDetailCommandHandler;
            _updateOrderDetailCommandHandler = updateOrderDetailCommandHandler;
            _removeOrderDetailCommandHandler = removeOrderDetailCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetailList()
        {
            var OrderDetailes = await _getOrderDetailQueryHandler.Handle();
            return Ok(OrderDetailes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> OrderDetailById(int id)
        {
            var OrderDetail = await _getOrderDetailByIdQueryHandler.Handle(new GetOrderDetailByIdQuery(id));
            if (OrderDetail == null)
            {
                return NotFound();
            }
            return Ok(OrderDetail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail([FromBody] CreateOrderDetailCommand createOrderDetailCommand)
        {
            if (createOrderDetailCommand == null)
            {
                return BadRequest("Invalid OrderDetail data.");
            }
            await _createOrderDetailCommandHandler.Handle(createOrderDetailCommand);
            return Ok("OrderDetail created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail([FromBody] UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            if (updateOrderDetailCommand == null)
            {
                return BadRequest("Invalid OrderDetail data.");
            }
            await _updateOrderDetailCommandHandler.Handle(updateOrderDetailCommand);
            return Ok("OrderDetail updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrderDetail(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid OrderDetail ID.");
            }
            await _removeOrderDetailCommandHandler.Handle(new RemoveOrderDetailCommand(id));
            return Ok("OrderDetail removed successfully.");
        }
    }
}
