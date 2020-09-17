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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsencesController : ControllerBase
    {
        public AbsencesController(MyContext myContext)
        {
            _context = myContext;
        }
        private readonly MyContext _context;
        [HttpPost]
        public IActionResult Create(AbsenceVm absenceVm)
        {

            if (absenceVm.Type == "masuk")
            {
                Absence _absence = new Absence()
                {
                    UserId = absenceVm.UserId,
                    TimeIn = DateTimeOffset.Now
                };

                _context.Absences.Add(_absence);
                _context.SaveChanges();

                return Ok("Anda sudah absen masuk");
            }
            else if (absenceVm.Type == "pulang")
            {
                var user = _context.Absences.Where(x => x.UserId == absenceVm.UserId).FirstOrDefault();

                if (user == null)
                {
                    return BadRequest("User belum absen");
                }
                else
                {
                    if (!user.TimeOut.Equals(null))
                    {
                        return BadRequest("Anda sudah absen pulang");
                    }
                }

                user.TimeOut = DateTimeOffset.Now;
                _context.Absences.Update(user);
                _context.SaveChanges();

                return Ok("Anda sudah absen pulang");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]

        public async Task<IActionResult> GetData()
        {
            var getData = await _context.Absences.ToListAsync();
            return Ok(getData);
        }

        [HttpGet("{Id}")]
        public IActionResult GetByUsers(string Id)
        {
            var getUsers = _context.Absences.Where(x => x.UserId == Id).ToList();

            if (getUsers == null)
            {
                return NotFound("Users belum pernah absen");
            }

            return Ok(getUsers);
        }

    }
}
