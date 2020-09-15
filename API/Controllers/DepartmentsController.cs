using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<Department, DepartmentRepo>
    {
        private readonly DepartmentRepo _repo;
        public DepartmentsController(DepartmentRepo departmentRepo) : base (departmentRepo)
        {
            this._repo = departmentRepo;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, Department department)
        {
            var item = await _repo.GetById(Id);
            item.Name = department.Name;
            var updated = await _repo.Update(item);
            if(updated != null)
            {
                return Ok("Data updated");
            }
            return BadRequest("Update data failed");
        }
    }
}
