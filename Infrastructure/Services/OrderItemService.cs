using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Labb2_Infrastructure.DTOExstension;
using Labb2_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Labb2_Infrastructure.Services
{
    //public class OrderItemService : IOrderitemService
    //{

    //    private readonly HttpClient _httpClient;

    //    public OrderItemService(IHttpClientFactory clientFactory)
    //    {
    //        _httpClient = clientFactory.CreateClient("Api");
    //    }

    //    public async Task<OrderItem> CreateOrderItemAsync(OrderItemDto itemdto)
    //    {
    //        var item = AutoMapper<OrderItemDto, OrderItem>.Map(itemdto);

    //        var response = await _httpClient.PostAsJsonAsync("api/orderitem", item);

    //        if (response.IsSuccessStatusCode)
    //        {
    //            return await response.Content.ReadFromJsonAsync<OrderItem>();
    //        }
    //        return null;
    //    }

    //    public async Task<bool> DeleteOrderItemAsync(int id)
    //    {
    //        var response = await _httpClient.DeleteAsync($"api/orderitem/{id}");
    //        return response.IsSuccessStatusCode;
    //    }

    //    public async Task<List<OrderItemDto>> GetAllOrdersItemAsync()
    //    {
    //        var response = await _httpClient.GetFromJsonAsync<IEnumerable<OrderItem>>("api/orderitem");

    //        if (response == null)
    //        {
    //            return new List<OrderItemDto>();
    //        }

    //        var result = AutoMapper<OrderItem, OrderItemDto>.MapListIenum(response).ToList();

    //        return result;
    //    }

    //    public async Task<OrderItemDto> GetOrderItemByIdAsync(int id)
    //    {
    //        var response = await _httpClient.GetFromJsonAsync<OrderItem>($"api/orderitem/{id}");

    //        if (response == null)
    //        {
    //            return null;
    //        }
    //        return AutoMapper<OrderItem, OrderItemDto>.Map(response);
    //    }

    //    public async Task<bool> UpdateOrderItemAsync(int id, OrderItemDto orderItemDto)
    //    {
    //        var orderItemToUpdate = await _httpClient.GetFromJsonAsync<OrderItem>($"api/orderitem/{id}");

    //        if (orderItemToUpdate == null)
    //        {
    //            return false;
    //        }

    //        AutoMapper<OrderItemDto, OrderItem>.Map(orderItemDto, orderItemToUpdate);

    //        var response = await _httpClient.PutAsJsonAsync($"api/orderitem/{id}", orderItemToUpdate);

    //        return response.IsSuccessStatusCode;
    //    }
    //}
}
