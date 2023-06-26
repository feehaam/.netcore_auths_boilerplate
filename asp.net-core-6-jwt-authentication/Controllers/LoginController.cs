using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using asp.net_core_6_jwt_authentication.Models;

namespace asp.net_core_6_jwt_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("/login/user")]
        public ActionResult<object> Authenticate()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "This is just a name representing extra information"),
                new Claim(ClaimTypes.Role, "user")
            };
            string token = CreateToken(claims);
            return Ok(token);
        }

        [HttpGet("/login/admin")]
        public ActionResult<object> Authenticate2()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "This is just a name representing extra information"),
                new Claim(ClaimTypes.Role, "admin")
            };
            string token = CreateToken(claims);
            return Ok(token);
        }

        [HttpGet("/login/moderator")]
        public ActionResult<object> Authenticate3()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "This is just a name representing extra information"),
                new Claim(ClaimTypes.Role, "moderator")
            };
            string token = CreateToken(claims);
            return Ok(token);
        }

        [HttpGet("/login/admin_and_moderator")]
        public ActionResult<object> Authenticate4()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "This is just a name representing extra information"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "moderator")
            };
            string token = CreateToken(claims);
            return Ok(token);
        }


        private string CreateToken(List<Claim> clm)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: clm,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
