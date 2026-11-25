
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket.API.Data;
using StockMarket.API.DTOs.Auth;
using StockMarket.API.Interfaces;
using StockMarket.API.Models;

namespace StockMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(
            ApplicationDbContext context,
            IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (existingUser != null)
                return BadRequest(new { message = "Email already registered." });

            var user = new AppUser
            {
                FullName = request.FullName.Trim(),
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);

            return Ok(new AuthResponse
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !user.IsActive ||
                !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new AuthResponse
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            });
        }
    }
}
