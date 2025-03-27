using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces
{
    public interface ICategoryService
    {
        Task<Product> CreateCategoryAsync(CategoryDto categoryDto);
        Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
        Task<Category> GetCategoryAsync(int id);
        Task<bool> DeleteCategoryAsync(int id);

        Task<bool> UpdateCategoryAsync(int id, CategoryDto categoryUpdateDto);
    }
}
