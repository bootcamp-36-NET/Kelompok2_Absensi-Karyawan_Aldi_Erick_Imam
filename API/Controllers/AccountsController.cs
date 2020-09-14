using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using Client.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly MyContext _context;

        public AccountsController(MyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(LoginVM login)
        {
            try
            {
                var userExist = _context.Users.Where(u => u.UserName == login.UserId).SingleOrDefault();
                if(userExist != null)
                {
                    return Ok("User Found");
                }
                return BadRequest("User does not exist");
            }
            catch (Exception)
            {
                return BadRequest("Error logging in");
            }
        }
    }
}