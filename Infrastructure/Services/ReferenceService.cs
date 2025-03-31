using Labb2_Infrastructure.DTOExstension;
using Labb2_Infrastructure.Repositories;
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
    public class ReferenceService : IReferenceService
    {
        private readonly ICategoryRepository _referenceRepository;

        public ReferenceService(ICategoryRepository referenceRepository)
        {
            _referenceRepository = referenceRepository;
        }
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var list = await _referenceRepository.GetCategoriesAsync();
            var listToDto = AutoMapper<Category, CategoryDto>.MapListIenum(list);
            return listToDto;
        }
    }
}
