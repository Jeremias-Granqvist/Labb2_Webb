using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderWithCustomerAndItemsAsync(int orderId);

        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<bool> DeleteOrderAsync(int id);
        Task<Order> UpdateOrderAsync(int id, Order order);

    }
}
