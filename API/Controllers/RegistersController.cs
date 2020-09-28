using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.ViewModel;
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

        public async Task<IActionResult> Post(RegistersVm registersVm )
        {
            //try
            //{
                var isExist = _context.Users.Where(Q => Q.Email == registersVm.Email || Q.UserName == registersVm.Name).FirstOrDefault();
                if (isExist == null)
                {
                    var user = new User
                    {
                        UserName = registersVm.Username,
                        Email = registersVm.Email,

                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(registersVm.Password),
                        PhoneNumber = registersVm.Phone,
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0
                    };
                    await _context.Users.AddAsync(user);
                    var uRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = "1"
                    };
                    await _context.UserRoles.AddAsync(uRole);
                    var emp = new Employee
                    {
                        EmployeeId = user.Id,
                        Name = registersVm.Username,
                        Address = registersVm.Address,
                        CreateDate = DateTimeOffset.Now,
                        isDelete = false
                    };
                    _context.Employees.Add(emp);
                    _context.SaveChanges();
                    return Ok("Created Successfully");
                }
                else
                {
                    return BadRequest("Employee has been registered");
                }
                
            //}
            //catch(Exception)
            //{
            //    return BadRequest("Error");
            //}
        }

    }
}