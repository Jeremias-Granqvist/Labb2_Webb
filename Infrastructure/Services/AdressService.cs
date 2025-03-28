using Labb2_Infrastructure.DTOExstension;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Services
{
    public class AdressService : IAdressService
    {
        private readonly IRepository<Adress> _repository;

        public AdressService(IRepository<Adress> repository)
        {
            _repository = repository;
        }
        public async Task<Adress> CreateAdressAsync(AdressDto adressDto)
        {
            var adress = adressDto.AdressToEntity();
            return await _repository.AddAsync(adress);
        }

        public async Task<bool> DeleteAdressAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<AdressDto> GetAdressByIdAsync(int id)
        {
            var adress = await _repository.GetByIdAsync(id);
            if (adress == null) return null;
            return new AdressDto
            {
                AdressId = adress.AdressId,
                StreetName = adress.StreetName,
                City = adress.City,
                ZipCode = adress.ZipCode,
                Country = adress.Country,
                Customers = EntityToDto.CustomerToDto(adress.Customers).ToList()
            };
        }

        public async Task<IEnumerable<AdressDto>> GetAllAdressAsync()
        {
            return (await _repository.GetAllAsync()).AdressToDto();
        }

        public async Task<bool> UpdateAdressAsync(int id, AdressDto adressDto)
        {
            var adressToUpdate = await _repository.GetByIdAsync(id);
            if (adressToUpdate == null)
            {
                return false;
            }

            adressToUpdate.UpdateAdressFromDto(adressDto);

            return await _repository.UpdateAsync(adressToUpdate);
        }
    }
}
