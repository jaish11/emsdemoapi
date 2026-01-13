using emsdemoapi.Data;
using emsdemoapi.Data.DTO;
using emsdemoapi.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace emsdemoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthsController> _logger;
        public AuthsController(ILogger<AuthsController> logger, IConfiguration config,AppDbContext context)
        {
            _context = context;
            _config = config;
            _logger = logger;
        }

        #region Register
        [HttpPost ("register")]
        public IActionResult Register([FromBody] UserRegisterDto userDto)
        {
           if(_context.Users.Any(u=>u.Email==userDto.Email))
            {
                return BadRequest("User with this email already exists.");
            }
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User registered successfully.");
        }
        #endregion Register

        #region Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userDto)
        {
            User user = new User();
            try
            {
                user = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = GenerateJwtToken(user);
            return Ok(new
            {
                token,
                role = user.Role,
                email = user.Email,
                name = user.Name
            });
            //return Ok(new { token });
        }
        #endregion Login
        #region GetAllUsers
        [HttpGet("all")]
        [Authorize] // Only logged-in users can see all users
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.Role
            }).ToList();

            return Ok(users);
        }
        #endregion GetAllUsers

        #region ChangeUserRole
        [HttpPut("changerole/{userId}")]
        [Authorize(Roles = "Admin")] // Only Admin can change user role
        public IActionResult ChangeUserRole(int userId, [FromBody] string newRole)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return NotFound("User not found.");

            user.Role = newRole;
            _context.SaveChanges();

            return Ok($"User role updated to {newRole}");
        }
        #endregion ChangeUserRole
        #region GenerateToken
        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion GenerateToken
    }
}
