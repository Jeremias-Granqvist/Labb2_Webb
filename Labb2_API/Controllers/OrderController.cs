using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Labb2_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{


    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {

        _orderRepository = orderRepository;
    }
    //GET (hämta med API)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
    {
        try
        {
        var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }
        catch (Exception ex) { 


            return StatusCode(500, $"internal servier error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderWithCustomerAndItems(int id)
    {
        var order = await _orderRepository.GetOrderWithCustomerAndItemsAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }


    //POST (skapa med API)
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(Order order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _orderRepository.CreateOrderAsync(order);
        return Created();
    }

    //PUT (uppdatera med API)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, Order order)
    {
        await _orderRepository.UpdateOrderAsync(id, order);
        return Ok();
    }

    // DELETE (Ta bort med API)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var category = await _orderRepository.DeleteOrderAsync(id);
        if (category == true)
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

