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
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    // GET (hämta med API)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationUserDTO>>> GetAllUsers()
    {
        
        var customers = await _customerRepository.GetAllUserAsync();
        return Ok(customers);
    }

    [HttpGet("{id}/orders")]
    public async Task<IActionResult> GetUsersWithOrders(int id)
    {
        var result = _customerRepository.GetUsersWithOrdersAsync(id);
        return Ok(result);

    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserFromEmail(string email)
    {
        var result = await _customerRepository.GetUserFromEmailAsync(email);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsersWithAddress(int id)
    {
        var customer = await _customerRepository.GetUsersWithAdressAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    // POST (skapa med API)
    [HttpPost]
    public async Task<ActionResult<ApplicationUser>> CreateUser(ApplicationUser customer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var createdCustomer = await _customerRepository.CreateUserAsync(customer);

        if (createdCustomer == null)
        {
            return BadRequest("Failed to create user.");
        }

        return CreatedAtAction(nameof(GetUsersWithAddress), new { id = createdCustomer.UserId }, createdCustomer);
    }

    // PUT (uppdatera med API)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, ApplicationUser customer)
    {
        var updated = await _customerRepository.UpdateUserAsync(id, customer);
        if (updated == null)
        {
            return NotFound(); 
        }

        return Ok();
    }

    // DELETE (Ta bort med API)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _customerRepository.DeleteUserAsync(id);
        if (success)
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}