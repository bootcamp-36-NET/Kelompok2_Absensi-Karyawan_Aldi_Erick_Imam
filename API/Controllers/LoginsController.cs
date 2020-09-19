using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;

        public LoginsController(MyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> Post(LoginVm login)
        {
            try
            {
                var userExist = await _context.Users.Where(u => u.UserName == login.UserId).SingleOrDefaultAsync();
                if(userExist != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(login.Password, userExist.PasswordHash))
                    {
                        return Ok(new JwtSecurityTokenHandler().WriteToken(Token(userExist)));

                    }
                    return BadRequest("Wrong Password");
                }
                return BadRequest("User does not exist");
            }
            catch (Exception)
            {
                return BadRequest("Error logging in");
            }
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        public JwtSecurityToken Token(User user)

        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email),
                new Claim("Id", user.Id),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email),
                new Claim("Phone", user.PhoneNumber),
                new Claim("Role", "Employee")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"]
                , claims, expires: DateTime.UtcNow.AddMinutes(5), signingCredentials: signIn);
        }
    }
}