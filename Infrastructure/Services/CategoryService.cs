using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Infrastructure.DTOExstension;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository referenceRepository)
        {
            _repository = referenceRepository;
        }

        public Task<Product> CreateCategoryAsync(CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync()
        {
            var list = await _repository.GetCategoriesAsync();
            var listToDto = AutoMapper<Category, CategoryDto>.MapListIenum(list);
            return listToDto;
        }


        public Task<Category> GetCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategoryAsync(int id, CategoryDto categoryUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
