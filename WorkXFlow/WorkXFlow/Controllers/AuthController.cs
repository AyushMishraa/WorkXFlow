using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkXFlow.Models;
using WorkXFlow.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WorkXFlow.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WorkXFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _config = config;
            _roleManager = roleManager;
        }

        // For Registration
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // 👇 Agar role pass hua hai to usey assign kar
            if (!string.IsNullOrEmpty(dto.Role))
            {
                // Pehle check karo role exist karta hai ya nahi
                if (!await _roleManager.RoleExistsAsync(dto.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(dto.Role));
                }

                await _userManager.AddToRoleAsync(user, dto.Role);
            }
            else
            {
                // default role User
                await _userManager.AddToRoleAsync(user, "User");
            }

            // assign default role "User"
            //await _userManager.AddToRoleAsync(user, "User");

            return Ok("User registered successfully");
        }

        // For Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if(user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return Unauthorized("Invalid emmail or password");
            }

            // Generate JWT Token
            var token = await GenerateJwtToken(user);
            return Ok(token);
        }

        private async Task<AuthResponseDto> GenerateJwtToken(ApplicationUser user)
        {
            var jwtConfig = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // add roles in claim
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var token = new JwtSecurityToken(
                issuer: jwtConfig["Issuer"],
                audience: jwtConfig["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtConfig["DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

    }
}
