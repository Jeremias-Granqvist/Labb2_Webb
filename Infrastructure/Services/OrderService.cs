using Labb2_Infrastructure.DTOExstension;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;
        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<Order> CreateOrderAsync(OrderDto orderDto)
        {
            var order = orderDto.OrderToEntity();

            return await _repository.AddAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            return (await _repository.GetAllAsync()).OrderToDto();
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null) return null;
            return new OrderDto
            {
                OrderId = order.OrderId,
                OrderItems = order.OrderItems,
                DateOfOrder = order.DateOfOrder,
                Customer = order.Customer,
                CustomerId = order.CustomerId
            };
        }

        public async Task<bool> UpdateOrderAsync(int id, OrderDto orderDto)
        {
            var orderToUpdate = await _repository.GetByIdAsync(id);
            if (orderToUpdate == null)
            {
                return false;
            }
            orderToUpdate.UpdateOrderFromDto(orderDto);

            return await _repository.UpdateAsync(orderToUpdate);
        }
    }
}
