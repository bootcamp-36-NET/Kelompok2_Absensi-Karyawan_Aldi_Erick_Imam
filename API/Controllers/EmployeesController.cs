using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.ViewModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static API.ViewModel.UserVm;

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
        public async Task<IActionResult> Create(UserVm userVm) {
            if (ModelState.IsValid)
            {
                var stamp = new Guid();
                var uId = new Guid();
                var user = new User

                {
                    //Id = uId.ToString(),
                    UserName = userVm.Username,
                    Email = userVm.Email,
                    SecurityStamp = stamp.ToString(),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(userVm.Password),
                    PhoneNumber = userVm.Phone,
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
                    CreateDate = DateTimeOffset.Now,
                    isDelete = false
                };
                await _context.Employees.AddAsync(emp);
                var addUser= _context.SaveChangesAsync();
                return Ok(addUser+("Mantap"));
            }
            
            return BadRequest("Failed");
        }

        [HttpGet]
        public async Task<List<Employee>> GetAll()
        {
            List<Employee> list = new List<Employee>();
            //var user = new UserVM();
            var getData = await _context.Employees.Where(x => x.isDelete == false).ToListAsync();
            if (getData.Count == 0)
            {
                return null;
            }
            foreach (var item in getData)
            {
                var emp = new Employee()
                {
                    EmployeeId = item.EmployeeId,
                    Name = item.Name,
                    Address = item.Address,
                    Phone = item.Phone,
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate
                };
                list.Add(emp);
            }
            return list;
        }

        //[Authorize]
        [HttpGet("{id}")]
        public Employee GetID(string id)
        {
            var getData = _context.Employees.SingleOrDefault(x => x.EmployeeId == id);
            if (getData == null)
            {
                return null;
            }
            var emp = new Employee()
            {
                EmployeeId = getData.EmployeeId,
                Name = getData.Name,
                Address = getData.Address,
                Phone = getData.Phone,
                CreateDate = getData.CreateDate,
                UpdateDate = getData.UpdateDate
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
        public IActionResult Update(string id, EmployeeVm employeeVm) {
            if (ModelState.IsValid)
            {
                var getData = _context.Employees.Find(id);
                getData.EmployeeId = employeeVm.EmployeeId;
                getData.Name = employeeVm.Name;
                getData.Phone = employeeVm.Phone;
                getData.Address = employeeVm.Address;
                getData.UpdateDate = DateTimeOffset.Now;
                getData.isDelete = false;

                _context.Employees.Update(getData);
                _context.SaveChanges();
                return Ok("Successfuly Updated");
            }
            return BadRequest("Update Failed");
        }
    }
}