using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Labb2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        //GET (hämta med API)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetOrderAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderbyId(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            return order;
        }

        //POST (skapa med API)
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.CreateOrderAsync(order);
            return Created();
        }

        //PUT (uppdatera med API)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDto order)
        {
            await _orderService.UpdateOrderAsync(id, order);
            return Ok();
        }

        // DELETE (Ta bort med API)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var category = await _orderService.DeleteOrderAsync(id);
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

}

