using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.DTOExstension
{
    public static class DtoToEntity
    {
        public static Product ProductToEntity(this ProductDto productDTO)
        {
            var product = new Product
            {
                ProductName = productDTO.Name,
                ProductDescription = productDTO.Description,
                Price = productDTO.Price,
                Status = productDTO.Status,
                ProductCategoryId = productDTO.CategoryId
            };
            return product;
        }
        
        public static void UpdateFromDTO(this Product updated, ProductUpdateDto dto)
        {
            updated.ProductName = dto.Name;
            updated.ProductDescription = dto.Description;
                updated.Price = dto.Price;
            updated.ProductCategoryId = dto.CategoryId;
            updated.Status = dto.Status;
        }
    }
}
