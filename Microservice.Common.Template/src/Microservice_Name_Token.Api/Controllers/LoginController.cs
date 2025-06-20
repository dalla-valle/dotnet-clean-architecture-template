namespace Microservice_Name_Token.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

/// <summary>
/// Controller responsible for handling authentication and login requests.
/// </summary>
[ApiController]
[Route("auth")]
public class LoginController : ControllerBase
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginController"/> class.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    public LoginController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Authenticates the user and returns a JWT token if credentials are valid.
    /// </summary>
    /// <param name="request">The login request containing username and password.</param>
    /// <returns>An IActionResult containing the JWT token if successful, or Unauthorized if not.</returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Demo: Replace with real user validation
        if (request.Username == "admin" && request.Password == "password")
        {
            var token = this.GenerateJwtToken(request.Username);
            return this.Ok(new { token });
        }

        return this.Unauthorized();
    }

    private string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"] ?? "supersecretkey12345678901234567890123456789"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: this.configuration["Jwt:Issuer"] ?? "MyIssuer",
            audience: this.configuration["Jwt:Audience"] ?? "MyAudience",
            claims: claims,
            expires: DateTime.Now.AddMinutes(int.TryParse(this.configuration["Jwt:ExpireMinutes"], out var minutes) ? minutes : 60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

/// <summary>
/// Represents a login request with username and password.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string? Password { get; set; }
}