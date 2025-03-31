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
            var adress = AutoMapper<AdressDto, Adress>.Map(adressDto);
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
            return AutoMapper<Adress, AdressDto>.Map(adress);
        }

        public async Task<IEnumerable<AdressDto>> GetAllAdressAsync()
        {
            var list = await _repository.GetAllAsync();
            var changedList = AutoMapper<Adress, AdressDto>.MapListIenum(list);
            return changedList;
        }

        public async Task<bool> UpdateAdressAsync(int id, AdressDto adressDto)
        {
            var adressToUpdate = await _repository.GetByIdAsync(id);
            if (adressToUpdate == null)
            {
                return false;
            }
            AutoMapper<AdressDto, Adress>.Map(adressDto, adressToUpdate);

            return await _repository.UpdateAsync(adressToUpdate);
        }
    }
}
