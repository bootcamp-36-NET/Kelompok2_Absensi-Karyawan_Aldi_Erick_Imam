using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionsController : BaseController<Divisions, DivisionRepo>
    {
        private readonly DivisionRepo _repo;
        public DivisionsController(DivisionRepo divisionRepo) : base(divisionRepo)
        {
            this._repo = divisionRepo;
        }

        [HttpPut("{Id}", Name = "update")]
        public async Task<IActionResult> Update(int Id, Divisions divisions)
        {
            var item = await _repo.GetById(Id);
            item.Name = divisions.Name;
            item.updatedDate = DateTimeOffset.Now;
            item.DepartmentId = divisions.DepartmentId;

            var result = await _repo.Update(item);
            if(result != null)
            {
                return Ok("Data updated!");
            }
            return BadRequest("Update data failed!");
        }
    }
}
