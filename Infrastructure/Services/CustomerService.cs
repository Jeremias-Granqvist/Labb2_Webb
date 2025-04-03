using Labb2_Infrastructure.DTOExstension;
using Labb2_Infrastructure.UoW;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly HttpClient _httpClient;

        public CustomerService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Api");
        }
        public async Task<ApplicationUser> CreateUserAsync(ApplicationUserDTO customerDto)
        {
            var customer = AutoMapper<ApplicationUserDTO, ApplicationUser>.Map(customerDto);

            var response = await _httpClient.PostAsJsonAsync("api/customer", customer);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApplicationUser>();
            }
            return null;

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/customer/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ApplicationUserDTO>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<ApplicationUser>>("api/customer");

            if (response == null)
            {
                return new List<ApplicationUserDTO>();
            }

            var result = AutoMapper<ApplicationUser, ApplicationUserDTO>.MapListIenum(response).ToList();

            return result;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            try
            {
            var response = await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/customer/{email}");

            return response;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR SAYS : {ex.Message}");
                throw;
            }
        }

        public async Task<ApplicationUserDTO> GetUsersByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/customer/{id}");

            if (response == null)
            {
                return null;
            }
            return AutoMapper<ApplicationUser, ApplicationUserDTO>.Map(response);
        }

        public async Task<ApplicationUser> GetUsersWithAdressAsync(int customerId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/customer/{customerId}/address");

            return response;
        }

        public async Task<ApplicationUser> GetUsersWithOrdersAsync(int customerId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/customer/{customerId}/orders");

            return response;
        }

        public async Task<bool> UpdateUserAsync(int id, ApplicationUserDTO customerDto)
        {
            var customerToUpdate = await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/customer/{id}");

            if (customerToUpdate == null)
            {
                return false;
            }

            AutoMapper<ApplicationUserDTO, ApplicationUser>.Map(customerDto, customerToUpdate);

            var response = await _httpClient.PutAsJsonAsync($"api/customer/{id}", customerToUpdate);

            return response.IsSuccessStatusCode;

        }
    }
}
