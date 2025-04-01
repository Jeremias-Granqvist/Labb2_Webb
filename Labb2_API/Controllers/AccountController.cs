using Labb2_Infrastructure.Authentication.Repos;
using Microsoft.AspNetCore.Mvc;
using static Labb2_Infrastructure.Authentication.Responses.CustomResponses;
using Labb2_Infrastructure.Authentication.DTOs;

namespace Labb2_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccount _repo;
    public AccountController(IAccount accountRepo)
    {
        _repo = accountRepo;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegisterDTO model)
    {
        var result = await _repo.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginDTO model)
    {
        var result = await _repo.LoginAsync(model);
        return Ok(result);
    }
}
