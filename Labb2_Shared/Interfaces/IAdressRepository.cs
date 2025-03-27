using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces
{
    public interface IAdressRepository
    {
        Task<Adress> CreateAdressAsync(Adress adress);

        Task<IEnumerable<Adress>> GetAllAdressAsync();
        Task<Adress> GetAdressByIdAsync(int id);
        Task<bool> DeleteAdressAsync(int id);
    }
}
