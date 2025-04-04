using Labb2_Infrastructure.Authentication.Repos;
using Microsoft.AspNetCore.Mvc;
using static Labb2_Infrastructure.Authentication.Responses.CustomResponses;
using Labb2_Infrastructure.Authentication.DTOs;
using Microsoft.AspNetCore.Authorization;

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
    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="model" >The registration data for the new user, including first name, last name, email, etc.</param>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegisterDTO model)
    {
        var result = await _repo.RegisterAsync(model);
        return Ok(result);
    }

    /// <summary>
    /// Logs user in to website
    /// </summary>
    /// <param name="model">The login data for the user, including username/email and password.</param>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginDTO model)
    {
        var result = await _repo.LoginAsync(model);
        return Ok(result);
    }
    /// <summary>
    /// Non-functioning at the moment
    /// </summary>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(new { message = "logged out successfully" });
    }

    /// <summary>
    /// Refreshes a users JWT if they're logged in.
    /// </summary>
    /// <param name="model">The user's session information, which is used to validate and refresh the JWT token.</param>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public ActionResult<LoginResponse> RefreshToken(UserSession model)
    {
        var result = _repo.RefreshToken(model);
        return Ok(result);
    }
}
