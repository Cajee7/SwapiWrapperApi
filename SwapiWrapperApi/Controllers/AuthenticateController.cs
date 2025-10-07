using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using SwapiWrapperApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SwapiWrapperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            if(model.username == "admin"  && model.password == "password")
            {
                if (string.IsNullOrEmpty(model.username) || string.IsNullOrEmpty(model.password))
                {
                    return BadRequest("Username and password are required");
                }

                var token = generateToken(model.username);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings: ExpiryMinutes"]))
                };
                Response.Cookies.Append("jwt", token, cookieOptions);

                return Ok(new
                {
                    message = "Logged in successfully",
                    token = token
                });
            }
            return Unauthorized("Invalid Username or Password");
        }

        private string generateToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
