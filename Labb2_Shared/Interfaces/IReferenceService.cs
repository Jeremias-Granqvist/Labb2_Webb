using Labb2_Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces
{
    interface IReferenceService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
