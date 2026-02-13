using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionTracker.Api.Data;
using QuestionTracker.Api.Models;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuestionTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private static readonly HashSet<string> AllowedEmailDomains = new()
    {
        "gmail.com",
        "outlook.com",
        "hotmail.com",
        "yahoo.com",
        "proton.me",
        "protonmail.com",
        "icloud.com",
        "zoho.com",
        "rediffmail.com"
    };

    private readonly ApplicationDbContext _db;
    private readonly IConfiguration _config;

    private readonly EmailService _emailService;

    public AuthController(ApplicationDbContext db, IConfiguration config, EmailService emailService)
    {
        _db = db;
        _config = config;
        _emailService = emailService;
    }

    [HttpPost("register-start")]
    public async Task<IActionResult> RegisterStart(User user)
    {
        if (await _db.Users.AnyAsync(u => u.Email == user.Email))
            return BadRequest("User already exists");

        user.PasswordHash = HashPassword(user.PasswordHash);
        user.IsEmailVerified = false;
        user.EmailVerificationCode = GenerateOtp();
        user.EmailVerificationExpiry = DateTime.UtcNow.AddMinutes(10);

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        await _emailService.SendVerificationCode(
            user.Email,
            user.EmailVerificationCode
        );

        return Ok();
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp(VerifyOtpDto dto)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null)
            return BadRequest("Invalid email");

        if (user.EmailVerificationCode != dto.Code)
            return BadRequest("Invalid code");

        if (user.EmailVerificationExpiry < DateTime.UtcNow)
            return BadRequest("Code expired");

        user.IsEmailVerified = true;
        user.EmailVerificationCode = null;
        user.EmailVerificationExpiry = null;

        await _db.SaveChangesAsync();

        return Ok();
    }


    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(User login)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Email == login.Email);

        if (user == null)
            return Unauthorized();

        if (!user.IsEmailVerified)
            return Unauthorized("Email not verified");

        var hash = HashPassword(login.PasswordHash);

        if (user.PasswordHash != hash)
            return Unauthorized();

        var token = GenerateJwtToken(user);

        return Ok(new
        {
            token = token,
            userId = user.Id,
            email = user.Email
        });
    }


    private string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private string GenerateOtp()
    {
        var rnd = new Random();
        return rnd.Next(100000, 999999).ToString();
    }


}
