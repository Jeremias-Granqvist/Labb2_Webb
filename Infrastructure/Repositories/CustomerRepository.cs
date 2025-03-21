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
    class CustomerRepository : ICustomerRepository
    {
        private readonly StoreContext _context;
        public CustomerRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
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
    }
}
