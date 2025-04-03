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
        public async Task<ApplicationUser> GetUsersWithAdressAsync(int customerId)
        {
            return await _context.Users
                .Include(c => c.Adress)  // Eagerly load the Address
                .FirstOrDefaultAsync(c => c.UserId == customerId);
        }
        public async Task<ApplicationUser> GetUsersWithOrdersAsync(int customerId)
        {
            return await _context.Users
                .Include(c => c.Adress)  // Eagerly load the Address
                .Include(c => c.Orders)   // Eagerly load the Orders
                .ThenInclude(o => o.OrderItems)  // Eagerly load OrderItems
                .ThenInclude(oi => oi.Product)  // Eagerly load Products for OrderItems
                .FirstOrDefaultAsync(c => c.UserId == customerId);
        }

        public async Task<ApplicationUser> CreateUserAsync(ApplicationUser customer)
        {
            _context.Users.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public IQueryable<ApplicationUser> GetQueryable()
        {
            return _context.Set<ApplicationUser>().AsQueryable();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var customer = _context.Users.FindAsync(id);
            if (customer == null)
            {
                return false;
            }
            _context.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users
                .Include(c => c.Orders)
                .Include(a => a.Adress)
                .ToListAsync();
        }

        public async Task<ApplicationUser> GetUserFromEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<ApplicationUser> UpdateUserAsync(int id, ApplicationUser user)
        {
            var userToUpdate = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (userToUpdate == null)
            {
                return Task.FromResult<ApplicationUser>(null);
            }
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.PhoneNumber= user.PhoneNumber;

            _context.SaveChangesAsync();
            return Task.FromResult(userToUpdate);
        }
    }
}
