using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.DTOExstension
{
    public static class EntityToDto
    {
        public static IEnumerable<CategoryDto> CategoriesToDto(this IEnumerable<Category> categories)
        {
            var result = new List<CategoryDto>();

            foreach (var category in categories)
            {
                result.Add(new CategoryDto { Id = category.CategoryId, Name = category.CategoryName });
            }
            return result;
        }

        public static IEnumerable<ProductDto> ProductToDto(this IEnumerable<Product> products)
        {
            var productList = new List<ProductDto>();
            foreach (var product in products)
            {
                productList.Add(new ProductDto
                {
                    Id = product.ProductId,
                    CategoryId = product.ProductCategoryId,
                    Description = product.ProductDescription,
                    Name = product.ProductName,
                    Price = product.Price,
                    Status = product.Status
                });
            }
            return productList;
        }

        public static IEnumerable<CustomerDto> CustomerToDto(this IEnumerable<Customer> customers)
        {
            var customerList = new List<CustomerDto>();
            foreach (var customer in customers)
            {
                customerList.Add(new CustomerDto
                {
                    CustomerId = customer.CustomerId,
                    Firstname = customer.Firstname,
                    Lastname = customer.Lastname,
                    PhoneNo = customer.PhoneNo,
                    Email = customer.Email,
                    Adress = customer.Adress,
                    AdressId = customer.AdressId,
                    Orders = customer.Orders.Select(o => new OrderDto
                    {
                        OrderId = o.OrderId,
                        DateOfOrder = o.DateOfOrder
                    }).ToList()
                });
            }
            return customerList;
        }
    }
}
