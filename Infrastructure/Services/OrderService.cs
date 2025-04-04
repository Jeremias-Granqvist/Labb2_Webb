using Labb2_Infrastructure.DTOExstension;
using Labb2_Infrastructure.UoW;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService(IHttpClientFactory clientFactory )
        {
            _httpClient = clientFactory.CreateClient("Api");
        }

        public async Task<bool> PlaceOrderAsync(string customerMail, List<int> productIds)
        {
            var request = new CreateOrder
            {
                customerMail = customerMail,
                productIds = productIds
            };

            var response = await _httpClient.PostAsJsonAsync("api/order/place-order", request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                Console.WriteLine($"error placing order {response.StatusCode}");
                return false;
            }
        }

        public async Task<Order> CreateOrderAsync(OrderDto orderDto)
        {
            var order = AutoMapper<OrderDto, Order>.Map(orderDto);
            var response = await _httpClient.PostAsJsonAsync("api/order", order);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Order>();
            }
            return null;


        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/order/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            try
            {

            var list = await _httpClient.GetFromJsonAsync<List<Order>>("api/order");
            var changedList = AutoMapper<Order, OrderDto>.MapListIenum(list);
            return changedList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR I SERVICE: {ex.Message}");
                throw;
            }
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Order>($"api/order/{id}");

            if (response == null)
            {
                return null;
            }
            return AutoMapper<Order, OrderDto>.Map(response);
        }

        public async Task<bool> UpdateOrderAsync(int id, OrderDto orderDto)
        {
            var orderToUpdate = await _httpClient.GetFromJsonAsync<Order>($"api/order/{id}");

            if (orderToUpdate == null)
            {
                return false;
            }

            AutoMapper<OrderDto, Order>.Map(orderDto, orderToUpdate);

            var response = await _httpClient.PutAsJsonAsync($"api/order/{id}", orderToUpdate);

            return response.IsSuccessStatusCode;
        }
    }
}
