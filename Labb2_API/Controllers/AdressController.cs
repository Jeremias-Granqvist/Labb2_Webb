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
    private readonly IAdressService _adressService;
    private readonly IAdressRepository _adressRepository;
    public AdressController(IAdressService adressService, IAdressRepository adressRepository)
    {
        _adressService = adressService;
        _adressRepository = adressRepository;
    }

    //GET (hämta med API)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Adress>>> GetAllAdress()
    {
        var adress = await _adressService.GetAllAdressAsync();
        return Ok(adress);
    }
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


    //POST (skapa med API)
    [HttpPost]
    public async Task<ActionResult<Adress>> CreateAdress(AdressDto adressDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _adressService.CreateAdressAsync(adressDto);
        return Created();
    }

    //PUT (uppdatera med API)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAdress(int id, AdressDto adressDto)
    {
        await _adressService.UpdateAdressAsync(id, adressDto);
        return Ok();
    }

    // DELETE (Ta bort med API)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdress(int id)
    {
        var adress = await _adressService.DeleteAdressAsync(id);
        if (adress == true)
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }

    }

}
