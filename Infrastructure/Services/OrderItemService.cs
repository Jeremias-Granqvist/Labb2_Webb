using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Labb2_Infrastructure.DTOExstension;
using Labb2_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Services
{
    class OrderItemService : IOrderitemService
    {

        private readonly IRepository<OrderItem> _repository;


        public OrderItemService(IRepository<OrderItem> repo)
        {
            _repository = repo;
        }

        public async Task<OrderItem> CreateOrderItemAsync(OrderItemDto itemdto)
        {
            var item = AutoMapper<OrderItemDto, OrderItem>.Map(itemdto);
            return await _repository.AddAsync(item);
        }

        public Task<bool> DeleteOrderItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderItemDto>> GetAllOrdersItemAsync()
        {
            var list = await _repository.GetAllAsync();
            var changedList = AutoMapper<OrderItem, OrderItemDto>.MapListIenum(list);
            return changedList;
        }

        public async Task<OrderItemDto> GetOrderItemByIdAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null) return null;
            return AutoMapper<OrderItem, OrderItemDto>.Map(order);
        }

        public async Task<bool> UpdateOrderItemAsync(int id, OrderItemDto orderItemDto)
        {
            var orderItemToUpdate = await _repository.GetByIdAsync(id);
            if (orderItemToUpdate == null)
            {
                return false;
            }
            AutoMapper<OrderItemDto, OrderItem>.Map(orderItemDto, orderItemToUpdate);

            return await _repository.UpdateAsync(orderItemToUpdate);
        }
    }
}
