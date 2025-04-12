using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			var user = new IdentityUser { UserName = model.Username, Email = model.Email };
			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				return Ok(new { Message = "User registered successfully!" });
			}

			return BadRequest(result.Errors);
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var user = await _userManager.FindByNameAsync(model.Username);
			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

				var token = GetToken(authClaims);
				return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = token.ValidTo });
			}

			return Unauthorized();
		}

		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

			return new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				expires: DateTime.Now.AddHours(3),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
			);
		}
	}

	public class RegisterModel
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}

	public class LoginModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
