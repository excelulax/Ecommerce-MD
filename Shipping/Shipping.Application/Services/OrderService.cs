using AutoMapper;
using Shipping.Application.Contracts;
using Shipping.Application.DTOs;
using Shipping.Domain.Contracts;
using Shipping.Domain.Entities;

namespace Shipping.Application.Services
{
    internal class OrderService : ICommonService<OrderDto, OrderInsertDto, OrderUpdateDto>
    {
        private readonly ICommonRepository<Order> _repository;
        private readonly IMapper _mapper;

        public OrderService(ICommonRepository<Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> Get()
        {
            var orders = await _repository.Get();
            var ordersDto = _mapper.Map<List<OrderDto>>(orders);
            return ordersDto;
        }

        public async Task<OrderDto> GetById(Guid id)
        {
            var order = await _repository.GetById(id);
            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }
        public async Task<OrderDto> Add(OrderInsertDto insertDto)
        {
            var order = _mapper.Map<Order>(insertDto);
            await _repository.Add(order);
            await _repository.Save();
            var orderResult = _mapper.Map<OrderDto>(order);
            return orderResult;
        }

        public async Task<OrderDto> Update(OrderUpdateDto updateDto)
        {
            var order = _mapper.Map<Order>(updateDto);
            _repository.Update(order);
            await _repository.Save();
            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }

        public async Task<OrderDto> Delete(Guid id)
        {
            var order = await _repository.GetById(id);
            if (order is not null)
            {
                _repository.Delete(order);
                await _repository.Save();
                return _mapper.Map<OrderDto>(order);
            }
            return null;
        }
    }
}
