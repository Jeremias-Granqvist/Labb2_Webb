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
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _context;

        public OrderRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;

        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var removeOrder = _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            if (removeOrder == null)
            {
                return false;
            }
            _context.Remove(removeOrder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
    }
}
