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

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var categories = await _referenceService.GetCategoriesAsync();
        return Ok(categories);
    }
}
