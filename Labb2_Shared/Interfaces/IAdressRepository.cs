using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces
{
    public interface IAdressRepository
    {
        Task<Adress> CreateAdressAsync(Adress adress);
        Task<Adress> GetAddressWithCustomersAsync(int addressId);
        Task<IEnumerable<Adress>> GetAllAdressAsync();
        Task<Adress> GetAdressByIdAsync(int id);
        Task<bool> DeleteAdressAsync(int id);

        Task<Adress> UpdateAdressAsync(int id, Adress adress);
    }
}
