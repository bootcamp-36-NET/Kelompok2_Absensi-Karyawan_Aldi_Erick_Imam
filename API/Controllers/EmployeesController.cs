using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public EmployeesController(MyContext myContext)
        {
            _context = myContext;
        }
        private readonly MyContext _context;
        // GET api/values

        

        [HttpPost]
        public async Task<IActionResult> Create(RegistersVm registersVm/*, EmployeeVm employeeVm*/) {
            if (ModelState.IsValid)
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
                return Ok("Mantap");
            }
            
            return BadRequest("Failed");
        }

        [HttpGet]
        public async Task<List<EmployeeVm>> GetAll()
        {
            var getData = await _context.Employees.Include("User").Where(x => x.isDelete == false).ToListAsync();
            //var getData = await _context.employees.Where(x => x.isDelete==false).ToListAsync();
            List<EmployeeVm> list = new List<EmployeeVm>();
            foreach (var employee in getData)
            {
                EmployeeVm emp = new EmployeeVm()
                {
                    EmployeeId = employee.User.Id,
                    Name = employee.Name,
                    Address = employee.Address,
                    Phone = employee.User.PhoneNumber,
                    CreateDate = employee.CreateDate,
                    UpdateDate = employee.UpdateDate,
                    DeleteData = employee.DeleteData
                };
                list.Add(emp);
            }
            return list;
        }

        //[Authorize]
        [HttpGet("{id}")]
        public EmployeeVm GetID(string id)
        {
            var getData = _context.Employees.Include("User").SingleOrDefault(x => x.EmployeeId == id);
            EmployeeVm emp = new EmployeeVm()
            {
                EmployeeId = getData.EmployeeId,
                Name = getData.Name,
                Address = getData.Address,
                Phone = getData.User.PhoneNumber,
                CreateDate = getData.CreateDate,
                UpdateDate = getData.UpdateDate,
                DeleteData = getData.DeleteData
            };
            return emp;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var getData = _context.Employees.SingleOrDefault(x => x.EmployeeId == id);
                if (getData == null)
                {
                    return BadRequest("Not Successfully");
                }
                getData.DeleteData = DateTimeOffset.Now;
                getData.isDelete = true;

                //_context.Employees.Update(getData);
                _context.Entry(getData).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok("Successfully Deleted");
            }
            return BadRequest("Not Successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Employee employee) {
            if (ModelState.IsValid)
            {
                var getData = _context.Employees.Find(id);
                getData.Name = employee.Name;
                getData.Phone = employee.Phone;
                getData.Address = employee.Address;
                getData.UpdateDate = DateTimeOffset.Now;
                getData.isDelete = false;
                
                _context.SaveChanges();
                return Ok("Successfuly Updated");
            }
            return BadRequest("Update Failed");
        }
    }
}