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
        
        public static void UpdateProductFromDTO(this Product updated, ProductDto dto)
        {
            updated.ProductName = dto.Name;
            updated.ProductDescription = dto.Description;
                updated.Price = dto.Price;
            updated.ProductCategoryId = dto.CategoryId;
            updated.Status = dto.Status;
        }

        public static void UpdateCustomerFromDTO(this Customer updated, CustomerDto dto)
        {
            updated.Firstname = dto.Firstname;
            updated.Lastname = dto.Lastname;
            updated.PhoneNo = dto.PhoneNo;
            updated.Email = dto.Email;
            updated.Adress = dto.Adress;
            updated.AdressId = dto.AdressId;
        }


        public static Customer CustomerToEntity(this CustomerDto customerDTO)
        {
            var customer = new Customer
            {
                Firstname = customerDTO.Firstname,
                Lastname = customerDTO.Lastname,
                PhoneNo = customerDTO.PhoneNo,
                Email = customerDTO.Email,
                Adress = customerDTO.Adress,
                AdressId = customerDTO.AdressId,
            };
            return customer ;
        }
    }
}
