//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using API.Context;
//using API.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmployeesController : ControllerBase
//    {
//        public EmployeesController(MyContext myContext)
//        {
//            _context = myContext;
//        }
//        private readonly MyContext _context;
//        // GET api/values
//        [HttpGet]
//        public async Task<List<Employee>> GetAll()
//        {
//            List<Employee> list = new List<Employee>();
//            //var user = new UserVM();
//            var getData = await _context.Employees.Include("User").Where(x => x.isDelete == false).ToListAsync();
//            if (getData.Count == 0)
//            {
//                return null;
//            }
//            foreach (var item in getData)
//            {
//                var emp = new Employee()
//                {
//                    EmployeeId = item.user.Id,
//                    Name = item.Name,
//                    Address = item.Address,
//                    Phone = item.Phone,
//                    CreateData = item.CreateData,
//                    UpdateDate = item.UpdateDate
//                };
//                list.Add(emp);
//            }
//            return list;
//        }

//        //[Authorize]
//        [HttpGet("{id}")]
//        public Employee GetID(string id)
//        {
//            var getData = _context.Employees.Include("User").SingleOrDefault(x => x.EmployeeId == id);
//            if (getData == null || getData.User == null)
//            {
//                return null;
//            }
//            var emp = new Employee()
//            {
//                EmployeeId = getData.User.Id,
//                Name = getData.Name,
//                Address = getData.Address,
//                Phone = getData.Phone,
//                CreateData = getData.CreateData,
//                UpdateDate = getData.UpdateDate
//            };
//            return emp;
//        }

//        [HttpDelete("{id}")]
//        public IActionResult Delete(string id)
//        {
//            if (ModelState.IsValid)
//            {
//                var getData = _context.Employees.Include("User").SingleOrDefault(x => x.EmployeeId == id);
//                if (getData == null)
//                {
//                    return BadRequest("Not Successfully");
//                }
//                getData.DeleteData = DateTimeOffset.Now;
//                getData.isDelete = true;

//                //_context.Employees.Update(getData);
//                _context.Entry(getData).State = EntityState.Modified;
//                _context.SaveChanges();
//                return Ok("Successfully Deleted");
//            }
//            return BadRequest("Not Successfully");
//        }
//    }
//}