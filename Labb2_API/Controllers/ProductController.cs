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

    /// <summary>
    /// returns a list of all products
    /// </summary>
    /// <returns>200 = returns a list of all products currently in the database.</returns>
    //GET (hämta med API)
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
    }


    /// <summary>
    /// Adds new product to database.
    /// </summary>
    /// <param name="product">The product object to be added to the database. This contains product details such as name, price, description, etc.</param>
    /// <returns>200 = new product added to database</returns>
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


    /// <summary>
    /// Updates product in database.
    /// </summary>
    /// <param name="id">The ID of the product to be updated. This ID is used to find the product in the database.</param>
    /// <param name="product">The product object containing updated details (such as name, price, etc.) to replace the existing product data.</param>
    /// <returns>
    /// <returns>200 = product was updated.</returns>
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

    /// <summary>
    /// deletes product with specified ID
    /// </summary>
    /// <param name="id">The ID of the product to be deleted. This ID is used to find and remove the product from the database.</param>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    // DELETE (Ta bort med API)
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
