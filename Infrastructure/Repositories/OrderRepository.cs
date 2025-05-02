using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
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

        public async Task<Order> GetOrderWithCustomerAndItemsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.User)  // Eagerly load the user
                .Include(o => o.Products) // Eagerly load Products
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }


        public async Task<Order> CreateOrderAsync(Order order)
        {
            foreach (var product in order.Products)
            {
                _context.Attach(product);
            }
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

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Products)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public Task<Order> UpdateOrderAsync(int id, Order order)
        {
            var orderToUpdate = _context.Orders.FirstOrDefault(o => o.OrderId == id);

            if (orderToUpdate == null)
            {
                return Task.FromResult<Order>(null);
            }
            orderToUpdate.DateOfOrder = order.DateOfOrder;
            orderToUpdate.Products = order.Products;

            _context.SaveChangesAsync();

            return Task.FromResult(orderToUpdate);
        }
    }
}
