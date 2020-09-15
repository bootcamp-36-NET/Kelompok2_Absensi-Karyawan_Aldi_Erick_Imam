using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly MyContext _context;

        public RegistersController(MyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            try
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                await _context.Users.AddAsync(user);
                var addUser = _context.SaveChangesAsync();
                return Ok(addUser);
            }
            catch(Exception)
            {
                return BadRequest("Error");
            }
        }

    }
}