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
        //Categories
        public static IEnumerable<CategoryDto> CategoriesToDto(this IEnumerable<Category> categories)
        {
            var result = new List<CategoryDto>();

            foreach (var category in categories)
            {
                result.Add(new CategoryDto { Id = category.CategoryId, Name = category.CategoryName });
            }
            return result;
        }

        //Products
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

        public static ProductDto TransformProductToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.ProductId,
                CategoryId = product.ProductCategoryId,
                Name = product.ProductName,
                Description = product.ProductDescription,
                Price = product.Price,
                Status = product.Status
            };

        }
        //Customers
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
                    Adress = customer.Adress != null ? TransformAdressToDto(customer.Adress) : null,
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


        public static CustomerDto TransformCustomerToDto(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "customer cannot be null");
            }
            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                Firstname = customer.Firstname,
                Lastname = customer.Lastname,
                Email = customer.Email,
                PhoneNo = customer.PhoneNo,
                AdressId = customer.AdressId,
                Adress = customer.Adress != null ? TransformAdressToDto(customer.Adress) : null

            };
        }

        //Order
        public static IEnumerable<OrderDto> OrderToDto(this IEnumerable<Order> orders)
        {
            var orderList = new List<OrderDto>();

            foreach (var order in orders)
            {
                var orderDto = new OrderDto
                {
                    OrderId = order.OrderId,
                    DateOfOrder = order.DateOfOrder,
                    CustomerId = order.CustomerId,
                    OrderItems = order.OrderItems.Select(item => TransformOrderItemToDto(item)).ToList(),
                    //Customer = TransformCustomerToDto(order.Customer)
                };

                orderList.Add(orderDto);
            }

            return orderList;
        }
        public static OrderDto TransformOrderToDto(Order order)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                DateOfOrder = order.DateOfOrder,
                CustomerId = order.CustomerId,
                //Customer = TransformCustomerToDto(order.Customer),
                OrderItems = TransformOrderItemsListToDto(order.OrderItems).ToList()
            };
        }


        //OrderItem
        public static OrderItemDto TransformOrderItemToDto(OrderItem item)
        {
            return new OrderItemDto
            {
                OrderItemId = item.OrderItemId,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price,
                Product = TransformProductToDto(item.Product)
            };
        }
        public static ICollection<OrderItemDto> TransformOrderItemsListToDto(ICollection<OrderItem> orderItems)
        {
            ICollection<OrderItemDto> orderItemDtos = new List<OrderItemDto>();
            foreach (var item in orderItems)
            {
                OrderItemDto orderItemDto = new OrderItemDto
                {
                    OrderId = item.OrderId,
                    OrderItemId = item.OrderItemId,
                    Price = item.Price,
                    Product = TransformProductToDto(item.Product),
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                orderItemDtos.Add(orderItemDto);
            }
            return orderItemDtos;
        }

        //Adress

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
                    Customers = CustomerToDto(adress.Customers).ToList()
                });
            }
            return adressList;
        }
        public static AdressDto TransformAdressToDto(Adress adress)
        {
            if (adress == null)
            {
                throw new ArgumentNullException(nameof(adress), "Adress cannot be null.");

            }
            return new AdressDto
            {
                AdressId = adress.AdressId,
                StreetName = adress.StreetName,
                City = adress.City,
                ZipCode = adress.ZipCode,
                Country = adress.Country
            };
        }

    }
}
