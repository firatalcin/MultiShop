using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResult;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class GetOrderDetailQueryHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        public GetOrderDetailQueryHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetOrderDetailQueryResult>> Handle()
        {
            var orderDetails = await _repository.GetAllAsync();
            var result = orderDetails.Select(od => new GetOrderDetailQueryResult
            {
                Id = od.Id,
                ProductId = od.ProductId,
                ProductName = od.ProductName,
                ProductPrice = od.ProductPrice,
                ProductAmount = od.ProductAmount,
                ProductTotalPrice = od.ProductTotalPrice,
                OrderingId = od.OrderingId
            }).ToList();
            return result;
        }
    }
}
