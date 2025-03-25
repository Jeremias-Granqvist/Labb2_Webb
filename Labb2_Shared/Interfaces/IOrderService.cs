using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(OrderDto orderDto);
        Task<IEnumerable<OrderDto>> GetOrderAsync();
        Task<Order> GetOrderByIdAsync(int id);

        Task<bool> DeleteOrderAsync(int id);

        Task<bool> UpdateOrderAsync(int id, OrderDto orderDto);
    }
}
