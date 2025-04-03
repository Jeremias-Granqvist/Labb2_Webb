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

namespace Labb2_Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly HttpClient _httpClient;

        public ProductService(IRepository<Product> repository, HttpClient httpClient)
        {
            _repository = repository;
            _httpClient = httpClient;
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            

            var product = AutoMapper<ProductDto, Product>.Map(productDto);

            return await _repository.AddAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var list = await _repository.GetAllAsync();
            var productDto = AutoMapper<Product, ProductDto>.MapListIenum(list);
            return productDto;
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDto productDto)
        {
            var productToUpdate = await _repository.GetByIdAsync(id);
            if (productToUpdate == null)
            {
                return false;
            }

            AutoMapper<ProductDto, Product>.Map(productDto, productToUpdate);

            return await _repository.UpdateAsync(productToUpdate);
        }
    }
}
