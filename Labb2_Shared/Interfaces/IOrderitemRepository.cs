using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces
{
    public interface IOrderitemRepository
    {
        Task<OrderItem> CreateOrderItemAsync(OrderItem orderitem);
        Task<IEnumerable<OrderItem>> GetAllOrdersItemsAsync();
        Task<OrderItem> GetOrderItemByIdAsync(int id);
        Task<bool> DeleteOrderItemAsync(int id);
    }
}
