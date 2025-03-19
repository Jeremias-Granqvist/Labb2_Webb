using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces;

public interface IProductService
{
    Task<Product> CreateProductAsync(ProductDto productDto);
    Task<IEnumerable<ProductDto>> GetProductsAsync();

    Task<bool> DeleteProductAsync(int id);

    Task<bool> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto);
}
