using AutoMapper;
using Labb2_Infrastructure.DTOExstension;
using Labb2_Infrastructure.Authentication.States;

using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Labb2_Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClient)
        {

            _httpClient = httpClient.CreateClient("Api");
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            var product = AutoMapper<ProductDto, Product>.Map(productDto);

            var response = await _httpClient.PostAsJsonAsync("api/product", product);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Product>();
            }
            return null;

        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/product/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Product>>("api/product");

            if (response == null)
            {
                return new List<ProductDto>();
            }

            var result = AutoMapper<Product, ProductDto>.MapListIenum(response).ToList();

            return result;
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDto productDto)
        {
            var productToUpdate = await _httpClient.GetFromJsonAsync<Product>($"api/product/{id}");

            if (productToUpdate == null)
            {
                return false;
            }

            productToUpdate = AutoMapper<ProductDto, Product>.Map(productDto, productToUpdate);

            var response = await _httpClient.PutAsJsonAsync($"api/product/{id}", productToUpdate);

            return response.IsSuccessStatusCode;
        }
    }
}
