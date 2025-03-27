using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static IEnumerable<OrderDto> OrderToDto(this IEnumerable<Order> orders)
        {
            var orderList = new List<OrderDto>();
            foreach (var order in orders)
            {
                orderList.Add(new OrderDto
                {
                    OrderId = order.OrderId,
                    OrderItems = order.OrderItems.Select(item => new OrderItemDto
                    {
                        OrderItemId = item.OrderItemId,
                        OrderId = item.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Product = new ProductDto
                        {
                            Id = item.Product.ProductId,
                            CategoryId = item.Product.ProductCategoryId,
                            Name = item.Product.ProductName,
                            Description = item.Product.ProductDescription,
                            Price = item.Product.Price,
                            Status = item.Product.Status
                        }
                    }).ToList(),
                    DateOfOrder = order.DateOfOrder,
                    CustomerId = order.CustomerId,
                    Customer = new CustomerDto
                    {
                        Firstname = order.Customer.Firstname,
                        Lastname = order.Customer.Lastname,
                        AdressId = order.Customer.AdressId,
                        CustomerId = order.Customer.CustomerId,
                        Email = order.Customer.Email,
                        PhoneNo = order.Customer.PhoneNo,
                        Orders = orders.Where(o => o.CustomerId == order.CustomerId)
                        .Select(o => new OrderDto
                        {
                            OrderId = o.OrderId,
                            DateOfOrder = o.DateOfOrder,
                            OrderItems = o.OrderItems.Select(item => new OrderItemDto
                            {
                                OrderItemId = item.OrderItemId,
                                ProductId = item.ProductId,
                                Price = item.Price,
                                Quantity = item.Quantity,
                                OrderId = item.OrderId,
                                Order = item.Order
                                Product = new ProductDto
                                {
                                    Id = item.Product.ProductId,
                                    CategoryId = item.Product.ProductCategoryId,
                                    Name = item.Product.ProductName,
                                    Description = item.Product.ProductDescription,
                                    Price = item.Product.Price,
                                    Status = item.Product.Status
                                }
                            })
                        }),
                        Adress = new AdressDto
                        {
                            AdressId = order.Customer.Adress.AdressId,
                            StreetName = order.Customer.Adress.StreetName,
                            City = order.Customer.Adress.City,
                            ZipCode = order.Customer.Adress.ZipCode,
                            Country = order.Customer.Adress.Country
                        }

                    },
                });
            }
            return orderList;
        }

        public static IEnumerable<AdressDto> AdressToDto(this IEnumerable<Adress> adresses)
        {
            var adressList = new List<AdressDto>();
            foreach (var adress in adresses)
            {
                adressList.Add(new AdressDto
                {
                    AdressId = adress.AdressId,
                    StreetName = adress.StreetName,
                    City = adress.City,
                    ZipCode = adress.ZipCode,
                    Country = adress.Country,
                    Customers = adress.Customers
                });
            }
            return adressList;
        }
    }
}
