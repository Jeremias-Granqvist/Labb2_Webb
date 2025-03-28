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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StoreContext _context;
        public CustomerRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<Customer> GetCustomerWithAdressAsync(int customerId)
        {
            return await _context.Customers
                .Include(c => c.Adress)  // Eagerly load the Address
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }
        public async Task<Customer> GetCustomerWithOrdersAsync(int customerId)
        {
            return await _context.Customers
                .Include(c => c.Adress)  // Eagerly load the Address
                .Include(c => c.Orders)   // Eagerly load the Orders
                .ThenInclude(o => o.OrderItems)  // Eagerly load OrderItems
                .ThenInclude(oi => oi.Product)  // Eagerly load Products for OrderItems
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public IQueryable<Customer> GetQueryable()
        {
            return _context.Set<Customer>().AsQueryable();
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }
            _context.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Customer>> GetCustomerAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.Orders)
                .Include(a => a.Adress)
                .ToListAsync();
        }


    }
}
