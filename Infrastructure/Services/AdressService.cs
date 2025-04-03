using Labb2_Infrastructure.DTOExstension;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Services
{
    public class AdressService : IAdressService
    {
        private readonly HttpClient _httpClient;
        public AdressService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("Api");
        }
        public async Task<Adress> CreateAdressAsync(AdressDto adressDto)
        {
            var adress = AutoMapper<AdressDto, Adress>.Map(adressDto);

            var response = await _httpClient.PostAsJsonAsync("api/adress", adress);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Adress>();
            }
            return null;
        }

        public async Task<bool> DeleteAdressAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/adress/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<AdressDto> GetAdressByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Adress>($"api/adress/{id}");

            if (response == null)
            {
                return null;
            }
            return AutoMapper<Adress, AdressDto>.Map(response);
        }

        public async Task<List<AdressDto>> GetAllAdressAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Adress>>("api/adress");

            if (response == null)
            {
                return new List<AdressDto>();
            }

            var result = AutoMapper<Adress, AdressDto>.MapListIenum(response).ToList();

            return result;
        }

        public async Task<bool> UpdateAdressAsync(int id, AdressDto adressDto)
        {
            var adressToUpdate = await _httpClient.GetFromJsonAsync<Adress>($"api/adress/{id}");

            if (adressToUpdate == null)
            {
                return false;
            }

            AutoMapper<AdressDto, Adress>.Map(adressDto, adressToUpdate);

            var response = await _httpClient.PutAsJsonAsync($"api/adress/{id}", adressToUpdate);

            return response.IsSuccessStatusCode;
        }

    }
}
