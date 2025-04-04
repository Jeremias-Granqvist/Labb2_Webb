using Labb2_Infrastructure.Repositories;
using Labb2_Infrastructure.Services;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Labb2_API.Controllers
{

    //[Route("api/[controller]")]
    //[ApiController]

    //public class OrderItemsController : ControllerBase
    //{
    //    private readonly IOrderitemService _repository;
    //    public OrderItemsController(IOrderitemService repo)
    //    {
    //        _repository = repo;
    //    }

    //    //GET (hämta med API)
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAllOrderItems()
    //    {
    //        try
    //        {
    //            var orders = await _repository.GetAllOrdersItemAsync();
    //            return Ok(orders);
    //        }
    //        catch (Exception ex)
    //        {


    //            return StatusCode(500, $"internal servier error: {ex.Message}");
    //        }
    //    }


    //    //POST (skapa med API)
    //    [HttpPost]
    //    public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItemDto orderItemDto)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }
    //        var result = await _repository.CreateOrderItemAsync(orderItemDto);
    //        return Created();
    //    }

    //    //PUT (uppdatera med API)
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutOrderItem(int id, OrderItemDto order)
    //    {
    //        await _repository.UpdateOrderItemAsync(id, order);
    //        return Ok();
    //    }

    //    // DELETE (Ta bort med API)
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteOrderItem(int id)
    //    {
    //        var category = await _repository.DeleteOrderItemAsync(id);
    //        if (category == true)
    //        {
    //            return Ok();
    //        }
    //        else
    //        {
    //            return NotFound();
    //        }
    //    }

    //}
}
