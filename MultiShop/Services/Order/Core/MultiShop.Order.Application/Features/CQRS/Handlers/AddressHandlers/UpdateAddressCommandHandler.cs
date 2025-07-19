using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class UpdateAddressCommandHandler
    {
        private readonly IRepository<Address> _repository;

        public UpdateAddressCommandHandler(IRepository<Address> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateAddressCommand updateAddressCommand)
        {
            var address = await _repository.GetByIdAsync(updateAddressCommand.Id);
            if (address != null)
            {
                address.UserId = updateAddressCommand.UserId;
                address.District = updateAddressCommand.District;
                address.City = updateAddressCommand.City;
                address.Detail = updateAddressCommand.Detail;
                await _repository.UpdateAsync(address);
            }
        }
    }
}
