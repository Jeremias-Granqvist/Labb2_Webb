using Labb2_Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Labb2_Shared;
using Labb2_Shared.Models;
using Labb2_Shared.Interfaces;
using Labb2_Infrastructure.Services;
using Labb2_Shared.Dtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Labb2_Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Labb2_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerRepository customerRepo, ICustomerService customerService)
    {
        _customerService = customerService;
    }

    //GET (hämta med API)
    [HttpGet]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllUsers()
    {
        var customer = await _customerService.GetAllUsersAsync();
        return Ok(customer);
    }
    [HttpGet("{id}/orders")]
    public async Task<IActionResult> GetUsersWithOrders(int id)
    {
        try
        {
            var customer = await _customerService.GetUsersWithOrdersAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            
                return Ok(customer.Orders);  // Return only the orders related to the customer
        }
        catch (Exception ex)
        {
            // Log the exception and return 500 if something goes wrong
            Console.WriteLine($"Error fetching orders for customer {id}: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsersWithAddress(int id)
    {
        var customer = await _customerService.GetUsersWithAdressAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    //POST (skapa med API)
    [HttpPost]
    public async Task<ActionResult<ApplicationUser>> CreateUser(ApplicationUserDTO customer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _customerService.CreateUserAsync(customer);
        return Created();
    }

    //PUT (uppdatera med API)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, ApplicationUserDTO customer)
    {
        await _customerService.UpdateUserAsync(id, customer);
        return Ok();
    }

    // DELETE (Ta bort med API)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var customer = await _customerService.DeleteUserAsync(id);
        if (customer == true)
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }

    }

}
