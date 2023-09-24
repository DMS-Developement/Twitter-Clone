using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Twitter_Clone.Data;
using Twitter_Clone.Models;
using Twitter_Clone.Models.AuthDTOs;
using Twitter_Clone.Services;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Twitter_Clone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly TwitterCloneDb _dbContext;
    private readonly UserMapper _userMapper;

    public AuthController(IConfiguration configuration, TwitterCloneDb dbContext, UserMapper userMapper)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _userMapper = userMapper;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDto userRegister)
    {
        var userExists = _dbContext.Users.Any(u => u.Username == userRegister.Username);
        if (userExists) return BadRequest("Username already exists");

        var hashedPassword = PasswordHasher.HashPassword(userRegister.Password);

        var newUser = new User
        {
            Username = userRegister.Username,
            Email = userRegister.Email,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            LastLogin = DateTime.UtcNow
        };

        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();

        var response = new RegisterResponse
        {
            Username = newUser.Username,
            Email = newUser.Email
        };

        return Created("", response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto userLogin)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == userLogin.Username);

        if (user == null || !PasswordHasher.VerifyPassword(userLogin.Password, user.PasswordHash)) return BadRequest("Invalid username or password");

        var userResponse = _userMapper.MapUserToDto(user);

        // Update LastLogin
        user.LastLogin = DateTime.UtcNow;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenExpiryTime = DateTime.Now.AddMinutes(1440);

        var token = new JwtSecurityToken(
            _configuration["JwtSettings:Issuer"],
            _configuration["JwtSettings:Audience"],
            claims,
            expires: tokenExpiryTime,
            signingCredentials: credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        var cookieOptions = new CookieOptions
        {
            Expires = tokenExpiryTime,
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None
        };

        Response.Cookies.Append("jwt_cookie", jwt, cookieOptions);

        return Ok(userResponse);
    }
}