using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpensesApp.Models;
using ExpensesApp.Repositories;
using ExpensesApp.Dtos;

namespace ExpensesApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthController(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    // ===== REGISTER USER =====
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
    {
        // Check if username already exists
        var existingUser = await _userRepository.GetByUsernameAsync(dto.Username);
        if (existingUser != null)
            return BadRequest("Username already exists.");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            // For demo purposes, you could store password as plain text
            // In production, hash the password before storing
        };

        await _userRepository.AddAsync(user);

        return Ok(user);
    }

    // ===== LOGIN USER =====
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null)
            return Unauthorized("Invalid username or password.");

        // Demo: plain text password check
        if (dto.Password != "password") // Replace with real hashed password check
            return Unauthorized("Invalid username or password.");

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    // ===== JWT TOKEN GENERATION =====
    private string GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}



