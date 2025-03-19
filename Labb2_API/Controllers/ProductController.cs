using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Labb2_Shared;
using Labb2_Shared.Models;
using Labb2_Infrastructure;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Dtos;

namespace Labb2_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }


    //GET (hämta med API)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _productService.GetProductsAsync();
            return Ok(products);
    }

    //[HttpGet("{id}")]
    //public async Task<ActionResult<Product>> GetProduct(int id)
    //{
    //    return null;
    //}

    //POST (skapa med API)
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(ProductDto product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _productService.CreateProductAsync(product);
        return Created();
    }

    //PUT (uppdatera med API)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, ProductUpdateDto product)
    {
        await _productService.UpdateProductAsync(id, product);
        return Ok();
    }

    // DELETE (Ta bort med API)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productService.DeleteProductAsync(id);
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
