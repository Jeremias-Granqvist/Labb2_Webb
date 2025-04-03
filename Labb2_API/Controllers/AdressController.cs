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

    //GET (hämta med API)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Adress>>> GetAllAdress()
    {
        var adress = await _adressRepository.GetAllAdressAsync();
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
    public async Task<ActionResult<Adress>> CreateAdress(Adress adress)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _adressRepository.CreateAdressAsync(adress);
        return Created();
    }

    //PUT (uppdatera med API)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAdress(int id, Adress adress)
    {
        
        await _adressRepository.UpdateAdressAsync(id, adress);
        return Ok();
    }

    // DELETE (Ta bort med API)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdress(int id)
    {
        var adress = await _adressRepository.DeleteAdressAsync(id);
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
