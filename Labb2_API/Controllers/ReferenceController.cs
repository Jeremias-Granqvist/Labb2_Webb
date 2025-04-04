using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Labb2_API.Controllers;

[Route("api/categories")]
[ApiController]
public class ReferenceController : ControllerBase
{
    private readonly IReferenceService _referenceService;

    public ReferenceController(IReferenceService referenceService)
    {
        _referenceService = referenceService;
    }

    /// <summary>
    /// testendpoint currently fetches all Categories to a list
    /// </summary>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var categories = await _referenceService.GetCategoriesAsync();
        return Ok(categories);
    }
}
