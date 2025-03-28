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
        private static Product TransformProductToEntity(ProductDto productDto)
        {
            return new Product
            {
                ProductId = productDto.Id,
                ProductName = productDto.Name,
                ProductDescription = productDto.Description,
                Price = productDto.Price,
                Status = productDto.Status,
                ProductCategoryId = productDto.CategoryId,
                
            };
        }

        //customer
        public static void UpdateCustomerFromDTO(this Customer updated, CustomerDto dto)
        {
            updated.Firstname = dto.Firstname;
            updated.Lastname = dto.Lastname;
            updated.PhoneNo = dto.PhoneNo;
            updated.Email = dto.Email;
            updated.Adress = TransformAdressToEntity(dto.Adress);
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
                Adress = TransformAdressToEntity(customerDTO.Adress),
                AdressId = customerDTO.AdressId,
            };
            return customer ;
        }

        private static Customer TransformCustomerToEntity(CustomerDto customer)
        {
            return new Customer
            {
                CustomerId = customer.CustomerId,
                Firstname = customer.Firstname,
                Lastname = customer.Lastname,
                PhoneNo = customer.PhoneNo,
                Email = customer.Email,
                Adress = TransformAdressToEntity(customer.Adress),
                AdressId = customer.AdressId,
            };
        }
        public static IEnumerable<Customer> TransformCustomersFromDto(this IEnumerable<CustomerDto> customers)
        {
            var customerList = new List<Customer>();
            foreach (var customer in customers)
            {
                customerList.Add(new Customer
                {
                    CustomerId = customer.CustomerId,
                    Firstname = customer.Firstname,
                    Lastname = customer.Lastname,
                    PhoneNo = customer.PhoneNo,
                    Email = customer.Email,
                    Adress = TransformAdressToEntity(customer.Adress),
                    AdressId = customer.AdressId,
                    Orders = customer.Orders.Select(o => new Order
                    {
                        OrderId = o.OrderId,
                        DateOfOrder = o.DateOfOrder
                    }).ToList()
                });
            }
            return customerList;
        }

        //Order
        public static Order OrderToEntity(this OrderDto orderDto)
        {
            var order = new Order
            {
                OrderId = orderDto.OrderId,
                OrderItems = TransformOrderItemListToEntity(orderDto.OrderItems),
                DateOfOrder = orderDto.DateOfOrder,
                Customer = TransformCustomerToEntity(orderDto.Customer),
                CustomerId = orderDto.CustomerId
            };
            return order;
        }
        public static void UpdateOrderFromDto(this Order updated, OrderDto dto)
        {
            updated.OrderId = dto.OrderId;
            updated.OrderItems = TransformOrderItemListToEntity(dto.OrderItems);
            updated.DateOfOrder = dto.DateOfOrder;
            updated.CustomerId = dto.CustomerId;
            updated.Customer = TransformCustomerToEntity(dto.Customer);
        }

        private static Order TransformOrderToEntity(OrderDto orderDto)
        {
            return new Order
            {
                OrderId = orderDto.OrderId,
                OrderItems = TransformOrderItemListToEntity(orderDto.OrderItems),
                DateOfOrder = orderDto.DateOfOrder,
                Customer = TransformCustomerToEntity(orderDto.Customer),
                CustomerId = orderDto.CustomerId
            };
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
                Customers = TransformCustomersFromDto(adressDto.Customers).ToList()
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
            updated.Customers = TransformCustomersFromDto(dto.Customers).ToList();
        }
        private static Adress TransformAdressToEntity(AdressDto adressDto)
        {
            return new Adress
            {
                AdressId = adressDto.AdressId,
                StreetName = adressDto.StreetName,
                City = adressDto.City,
                ZipCode = adressDto.ZipCode,
                Country = adressDto.Country,
                Customers = TransformCustomersFromDto(adressDto.Customers).ToList()
            };
        }

        //OrderItems
        public static OrderItem OrderItemToEntity(this OrderItemDto orderItemDto)
        {
            return new OrderItem
            {
                OrderId = orderItemDto.OrderId,
                OrderItemId = orderItemDto.OrderItemId,
                Price = orderItemDto.Price,
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
                Product = TransformProductToEntity(orderItemDto.Product)

            };
        }
        public static void UpdateOrderItemsFromDto(this OrderItemDto updated, OrderItemDto dto)
        {
            updated.OrderItemId = dto.OrderItemId;
            updated.OrderId = dto.OrderId;
            updated.ProductId = dto.ProductId;
            updated.Product = dto.Product;
            updated.Price = dto.Price;
            updated.Quantity = dto.Quantity;
        }
        private static OrderItem TransformOrderItemToEntity(OrderItemDto orderItemDto)
        {
            return new OrderItem
            {
                OrderId = orderItemDto.OrderId,
                OrderItemId = orderItemDto.OrderItemId,
                Price = orderItemDto.Price,
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
                Product = TransformProductToEntity(orderItemDto.Product)
            };
        }
        private static List<OrderItem> TransformOrderItemListToEntity(ICollection<OrderItemDto> orderItemDtoList)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var orderItemDto in orderItemDtoList)
            {
             OrderItem orderItem = new OrderItem
            {
                OrderId = orderItemDto.OrderId,
                OrderItemId = orderItemDto.OrderItemId,
                Price = orderItemDto.Price,
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
                Product = TransformProductToEntity(orderItemDto.Product)
            };
                orderItems.Add(orderItem);
            }
            return orderItems;
        }
    }
}
