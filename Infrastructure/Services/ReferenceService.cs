using Labb2_Infrastructure.DTOExstension;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly IReferenceRepository _referenceRepository;

        public ReferenceService(IReferenceRepository referenceRepository)
        {
            _referenceRepository = referenceRepository;
        }
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            return (await _referenceRepository.GetCategoriesAsync()).CategoriesToDto();
        }
    }
}
