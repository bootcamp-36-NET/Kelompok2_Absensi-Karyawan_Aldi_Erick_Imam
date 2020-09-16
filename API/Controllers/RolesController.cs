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
    public class RolesController : ControllerBase
    {
        private readonly MyContext _context;

        public RolesController(MyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Post(Role role)
        {
            try
            {
                role.NormalizedName = role.Name.ToUpper();
                await _context.Roles.AddAsync(role);
                _context.SaveChangesAsync();
                return Ok("Roles Added");
            }
            catch(Exception)
            {
                return BadRequest("Error");
            }
        }
    }
}