using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Repositories
{
    public class AdressRepository : IAdressRepository
    {
        private readonly StoreContext _context;

        public AdressRepository(StoreContext context)
        {
            _context= context;
        }

        public async Task<Adress> GetAddressWithCustomersAsync(int addressId)
        {
            return await _context.Adresses
                .Include(a => a.Customers)  // Eagerly load the Customers at the Address
                .FirstOrDefaultAsync(a => a.AdressId == addressId);
        }


        public async Task<Adress> CreateAdressAsync(Adress adress)
        {
            _context.Adresses.Add(adress);
            await _context.SaveChangesAsync();
            return adress;
        }

        public async Task<bool> DeleteAdressAsync(int id)
        {
            var adress = _context.Adresses.FindAsync(id);
            if (adress == null)
            {
                return false;
            }
            _context.Remove(adress);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Adress> GetAdressByIdAsync(int id)
        {
            var adress = _context.Adresses.FirstOrDefault(a => a.AdressId == id);
            return adress;
        }

        public async Task<IEnumerable<Adress>> GetAllAdressAsync()
        {
            return await _context.Adresses.Include(c => c.Customers).ToListAsync();        
        }
    }
}
