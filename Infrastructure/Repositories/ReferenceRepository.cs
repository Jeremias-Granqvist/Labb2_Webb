using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Repositories
{
    public class ReferenceRepository : IReferenceRepository
    {
        private readonly StoreContext _context;

        public ReferenceRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync() =>
            (await _context.Categories.ToListAsync());
    }
}
