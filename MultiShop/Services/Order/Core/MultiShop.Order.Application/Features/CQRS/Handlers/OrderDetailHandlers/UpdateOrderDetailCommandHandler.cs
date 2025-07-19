using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class UpdateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            var orderDetail = await _repository.GetByIdAsync(updateOrderDetailCommand.Id);
            if (orderDetail == null)
            {
                throw new Exception("Order detail not found");
            }
            orderDetail.ProductId = updateOrderDetailCommand.ProductId;
            orderDetail.ProductName = updateOrderDetailCommand.ProductName;
            orderDetail.ProductPrice = updateOrderDetailCommand.ProductPrice;
            orderDetail.ProductAmount = updateOrderDetailCommand.ProductAmount;
            orderDetail.ProductTotalPrice = updateOrderDetailCommand.ProductTotalPrice;
            orderDetail.OrderingId = updateOrderDetailCommand.OrderingId;
            await _repository.UpdateAsync(orderDetail);
        }
    }
}
