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

    /// <summary>
    /// Get a list of all customers
    /// </summary>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
    // GET (hämta med API)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationUserDTO>>> GetAllUsers()
    {
        var customers = await _customerRepository.GetAllUserAsync();
        return Ok(customers);
    }


    //[HttpGet("{id}/orders")]
    //public async Task<IActionResult> GetUsersWithOrders(int id)
    //{
    //    var result = _customerRepository.GetUsersWithOrdersAsync(id);
    //    return Ok(result);

    //}

    /// <summary>
    /// Get specific user from userID
    /// </summary>
    /// <param name="id">The ID of the user to retrieve from the database.</param>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
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

    /// <summary>
    /// Create new user and save to DB.
    /// </summary>
    /// <param name="customer">The `ApplicationUser` object containing the details of the user to be created, such as name, email, etc.</param>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
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

    /// <summary>
    /// updates customer information in database
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="customer">The `ApplicationUser` object containing the updated user information.</param>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
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
}