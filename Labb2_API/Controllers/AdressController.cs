using Labb2_Infrastructure.Repositories;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Labb2_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdressController : ControllerBase
{

    private readonly IAdressRepository _adressRepository;
    public AdressController(IAdressRepository adressRepository)
    {
        _adressRepository = adressRepository;
    }
    /// <summary>
    /// returns a list of adresses from DB
    /// </summary>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
    //GET (hämta med API)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Adress>>> GetAllAdress()
    {
        var adress = await _adressRepository.GetAllAdressAsync();
        return Ok(adress);
    }

    /// <summary>
    /// Get specific adress based on AdressId
    /// </summary>
    /// <param name="id">The unique identifier for the address to retrieve.</param>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddressWithCustomers(int id)
    {
        var address = await _adressRepository.GetAddressWithCustomersAsync(id);
        if (address == null)
        {
            return NotFound();
        }
        return Ok(address);
    }

    /// <summary>
    /// Adds mew adress to database
    /// </summary>
    /// <param name="adress">The address data to be added to the database, including street name, city, zip code, and country.</param>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
    //POST (skapa med API)
    [HttpPost]
    public async Task<ActionResult<Adress>> CreateAdress(Adress adress)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _adressRepository.CreateAdressAsync(adress);
        return Created();
    }

    /// <summary>
    /// update adress already in database
    /// </summary> 
    /// <param name="id">The unique identifier for the address that needs to be updated.</param>
    /// <param name="adress">The updated address data, including street name, city, zip code, and country.</param>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
    //PUT (uppdatera med API)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAdress(int id, Adress adress)
    {
        
        await _adressRepository.UpdateAdressAsync(id, adress);
        return Ok();
    }

}
