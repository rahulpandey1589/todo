using TodoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IOptions<JwtModel> options;

        public AuthenticationController(IOptions<JwtModel> options)
        {
            this.options = options;
        }

        [HttpGet]
        public async Task<IActionResult> ValidateUser(string userName, string password)
        {
            if (userName == "Test" && password == "Password")
            {
                // generate a valid json web token


                var tokenHandler = new JwtSecurityTokenHandler();

                SecurityToken token = GenerateSecurityToken(tokenHandler);

                var tokenOutput = tokenHandler.WriteToken(token);

                return Ok(tokenOutput);
            }

            return BadRequest();
        }


        private SecurityToken GenerateSecurityToken(JwtSecurityTokenHandler tokenHandler)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.Value.IssuerSigningKey));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityTokenDescriptor
                 = new SecurityTokenDescriptor()
                 {
                     Issuer = null,
                     Audience = null,
                     SigningCredentials = signingCredentials,
                     Expires = DateTime.Now.AddMinutes(30),
                     Subject = new ClaimsIdentity(GetClaims())
                 };

            var token = tokenHandler.CreateToken(securityTokenDescriptor);
            return token;
        }



        private IEnumerable<Claim> GetClaims()
        {
            IEnumerable<Claim> claims
                 = new Claim[]
                 {
                     new Claim(ClaimTypes.Name,"Test"),
                     new Claim("IsAdmin","false")
                 };

            return claims;

        }


    }
}
