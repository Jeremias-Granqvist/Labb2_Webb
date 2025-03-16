using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces
{
    interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);

        Task<IEnumerable<Product>> GetProductsAsync();

        Task<bool> DeleteProductAsync(int id);
    }
}
