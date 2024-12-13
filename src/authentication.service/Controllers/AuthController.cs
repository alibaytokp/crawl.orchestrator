using authentication.service.Interfaces;
using authentication.service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace authentication.service.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    private readonly ITokenService _tokenService;
    private readonly AdminCredentials _adminCredentials;

    public AuthController(ITokenService tokenService, IOptions<AdminCredentials> adminCredentials)
    {
        _tokenService = tokenService;
        _adminCredentials = adminCredentials.Value;
    }

    [HttpPost("login")]
    public IActionResult Login(AdminCredentials request)
    {
        Console.WriteLine($"Incoming Login Request: Username = {request.Username}, Password = {request.Password}");
        //TODO: arrange later
        if (request.Username == "ADMIN" && request.Password == "CRAWLER200")
        {
            var token = _tokenService.GenerateJwtToken(request.Username);
            Console.WriteLine($"Generated Token: {token}");
            return Ok(new { token });
        }

        Console.WriteLine("Invalid credentials. Returning 401.");
        return Unauthorized(new
        {
            type = "https://tools.ietf.org/html/rfc9110#section-15.5.2",
            title = "Unauthorized",
            status = 401,
            traceId = HttpContext.TraceIdentifier
        });
    }

}
