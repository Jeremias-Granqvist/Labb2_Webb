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
            // Log exception
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryAsync(id);

        return category;
    }

    //POST (skapa med API)
    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(CategoryDto category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _categoryService.CreateCategoryAsync(category);
        return Created();
    }

    //PUT (uppdatera med API)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, CategoryDto category)
    {
        await _categoryService.UpdateCategoryAsync(id, category);
        return Ok();
    }

    // DELETE (Ta bort med API)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _categoryService.DeleteCategoryAsync(id);
        if (category == true)
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}
