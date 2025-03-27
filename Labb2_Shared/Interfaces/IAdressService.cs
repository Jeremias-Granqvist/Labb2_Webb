using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces
{
    public interface IAdressService
    {
        Task<Adress> CreateAdressAsync(AdressDto adressDto);
        Task<IEnumerable<AdressDto>> GetAllAdressAsync();
        Task<AdressDto> GetAdressByIdAsync(int id);
        Task<bool> DeleteAdressAsync(int id);

        Task<bool> UpdateAdressAsync(int id, AdressDto adressDto);
    }
}
