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

namespace Labb2_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerRepository customerRepo, ICustomerService customerService)
    {
        _customerRepository = customerRepo;
        _customerService = customerService;
    }

    //GET (hämta med API)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        var customer = await _customerService.GetAllCustomerAsync();
        return Ok(customer);
    }
    [HttpGet("{id}/orders")]
    public async Task<IActionResult> GetCustomerWithOrders(int id)
    {
        try
        {
            var customer = await _customerRepository.GetCustomerWithOrdersAsync(id);
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
    public async Task<IActionResult> GetCustomerWithAddress(int id)
    {
        var customer = await _customerRepository.GetCustomerWithAdressAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    //POST (skapa med API)
    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerDto customer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _customerService.CreateCustomerAsync(customer);
        return Created();
    }

    //PUT (uppdatera med API)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, CustomerDto customer)
    {
        await _customerService.UpdateCustomerAsync(id, customer);
        return Ok();
    }

    // DELETE (Ta bort med API)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _customerService.DeleteCustomerAsync(id);
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
