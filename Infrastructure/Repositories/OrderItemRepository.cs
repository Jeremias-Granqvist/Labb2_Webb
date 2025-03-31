using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderitemRepository
    {
        private readonly StoreContext _context;
        public OrderItemRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderitem)
        {
            _context.OrderItems.Add(orderitem);
            await _context.SaveChangesAsync();
            return orderitem;
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var remove = _context.OrderItems.FirstOrDefaultAsync(oi => oi.OrderItemId == id);
            if (remove == null)
            {
                return false;
            }
            _context.Remove(remove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrdersItemsAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(oi => oi.OrderItemId == id);
        }
    }
}
