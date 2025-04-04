using Labb2_Infrastructure;
using Labb2_Infrastructure.Services;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Infrastructure.DTOExstension;
using Labb2_Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Labb2_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Get a list of all categories
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
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        try
        {
            var categories = await _categoryService.GetAllCategoryAsync();

            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
