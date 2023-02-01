using EShopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController() { }
        public IActionResult Login(Account account)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (account.UserName != "abc" || account.Password != "abc") { return BadRequest("Invalid"); }

            var secertKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyEShopSecurityKey"));
            var signInCredentials = new SigningCredentials(secertKey , SecurityAlgorithms.HmacSha256);

            var tokenOption = new JwtSecurityToken(
                issuer: "", 
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.Name , account.UserName) , 
                    new Claim(ClaimTypes.Role , "admin")
                },
                expires:DateTime.Now.AddMinutes(30) ,
                signingCredentials:signInCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

            return Ok(new {token = tokenString});
        }
    }
}
