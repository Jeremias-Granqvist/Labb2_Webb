using Labb2_Infrastructure.Services;
using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.DTOExstension
{
    public static class DtoToEntity
    {
        //product
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
        //customer
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

        //Order

        public static Order OrderToEntity(this OrderDto orderDto)
        {
            var order = new Order
            {
                OrderId = orderDto.OrderId,
                OrderItems = orderDto.OrderItems,
                DateOfOrder = orderDto.DateOfOrder,
                Customer = orderDto.Customer,
                CustomerId = orderDto.CustomerId
            };
            return order;
        }
        public static void UpdateOrderFromDto(this Order updated, OrderDto dto)
        {
            updated.OrderId = dto.OrderId;
            updated.OrderItems = dto.OrderItems;
            updated.DateOfOrder = dto.DateOfOrder;
            updated.CustomerId = dto.CustomerId;
            updated.Customer = dto.Customer;
        }

        //Adress
        public static Adress AdressToEntity(this AdressDto adressDto)
        {
            var adress = new Adress
            {
                AdressId = adressDto.AdressId,
                StreetName = adressDto.StreetName,
                City = adressDto.City,
                ZipCode = adressDto.ZipCode,
                Country = adressDto.Country,
                Customers = adressDto.Customers
            };
            return adress;
        }
        public static void UpdateAdressFromDto(this Adress updated, AdressDto dto)
        {
            updated.AdressId = dto.AdressId;
            updated.StreetName = dto.StreetName;
            updated.City = dto.City;
            updated.ZipCode = dto.ZipCode;
            updated.Country = dto.Country;
            updated.Customers = dto.Customers;
        }
    }
}
