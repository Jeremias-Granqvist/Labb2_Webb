using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Labb2_Shared;
using Labb2_Shared.Models;
using Labb2_Infrastructure;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Labb2_Infrastructure.DTOExstension;

namespace Labb2_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    public ProductController(IProductRepository productRepo)
    {
        _productRepository = productRepo;
    }


    //GET (hämta med API)
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
    }



    //POST (skapa med API)
    [HttpPost]
    [Route("/create-product")]
    public async Task<ActionResult> CreateProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _productRepository.CreateProductAsync(product);
        return Ok();
    }

    //PUT (uppdatera med API)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, [FromBody] Product product)
    {
        Console.WriteLine($"Received ID from URL: {id}");
        Console.WriteLine($"Received Product ID from body: {product.Id}");

        if (product == null)
        {
            return BadRequest("Product data is missing");
        }

        // Ensure that the product ID in the body is updated
        if (product.Id != id)
        {
            return BadRequest("Product ID mismatch");
        }

        // Update the product
        await _productRepository.UpdateProductAsync(id, product);

        return Ok();
    }

    // DELETE (Ta bort med API)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productRepository.DeleteProductAsync(id);
        if (product == true)
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
        
    }
}
